using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Model;

namespace Presentation.Controllers
{
    [Route("Accounts")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var response = await _authService.Login(loginDTO);
            if (response == null)
            {
                return Unauthorized(new { message = "Missing/invalid input" });
            }
            return Ok(response);
        }
    }
}
