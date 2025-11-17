using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service;

namespace WebApi.api.Controllers
{
    [ApiController]
    [Route("Account/Login")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {

            if (string.IsNullOrWhiteSpace(request.userName) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new
                {
                    errorCode = "HB40001",
                    message = "Missing/invalid input"
                });
            }

            var result = await _authService.LoginAsync(request.userName, request.Password);

            if (result == null)
            {
                return Unauthorized(new
                {
                    errorCode = "HB40101",
                    message = "Token missing/invalid"
                });
            }

            return Ok(result);
        }
    }
}
