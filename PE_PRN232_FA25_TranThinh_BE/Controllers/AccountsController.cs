using FA25Bear.DataAccess.Models.DTOs;
using FA25Bear.Service;
using Microsoft.AspNetCore.Mvc;

namespace PE_PRN232_FA25_TranThinh_BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : Controller
    {
        private readonly IJwtService _jwtTokenService;

        private IBearAccountService _service;

        public AccountsController(IJwtService jwtTokenService, IBearAccountService service)
        {
            _jwtTokenService = jwtTokenService;
            _service = service;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDTO lrdto)
        {
            var user = _service.CheckLogin(lrdto);
            // Validate user credentials (replace with your actual authentication logic)
            if (user != null)
            {
                var token = _jwtTokenService.GenerateToken(
                    userId: user.AccountId.ToString(),
                    email: user.Email,
                    roles: user.Role
                );

                // Return the token in the response, can change to LoginResponse model if needed
                return Ok(new LoginResponseDTO
                {
                    Token = token,
                    Role = user.Role
                });
            }

            return Unauthorized(new { message = "Invalid credentials" });
        }
    }
}
