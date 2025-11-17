using BOs.Response;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace FA25Bear.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IBearAccountRepo _bearAccountRepo;

        public AccountsController(IJwtService jwtService, IBearAccountRepo bearAccountRepo)
        {
            _jwtService = jwtService;
            _bearAccountRepo = bearAccountRepo;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid login credentials");
            }
            try
            {
                var loggedInAccount = _bearAccountRepo.GetSystemAccountByEmail(request.Email, request.Password);
                if (loggedInAccount == null)
                {
                    return Unauthorized("Invalid username or password");
                }
                var token = _jwtService.GenerateToken(
                    loggedInAccount.AccountId,
                    loggedInAccount.Email,
                    loggedInAccount.RoleId.ToString());

                var refreshToken = _jwtService.GenerateRefreshToken();

                return Ok(new LoginReponse
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    Expiration = DateTime.UtcNow.AddMinutes(60)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            }
        }

}
