using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Dtos;
using Service.Security;

namespace PRN232_SU25_SE184826.api.Controllers
{
    [ApiController]
    [Route("Accounts")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await authService.LoginAsync(request.UserName, request.Password);
            if (!result.Success)
            {
                return result.ErrorCode switch
                {
                    "HB40001" => BadRequest(new ErrorResponse(result.ErrorCode, result.ErrorMessage!)),
                    "HB40101" => Unauthorized(new ErrorResponse(result.ErrorCode, result.ErrorMessage!)),
                    "HB40301" => StatusCode(403, new ErrorResponse(result.ErrorCode, result.ErrorMessage!)),
                    _ => StatusCode(500, new ErrorResponse("HB50001", result.ErrorMessage ?? "Internal server error"))
                };
            }
            return Ok(new LoginResponse { Token = result.Token!, Role = result.Role! });
        }
    }
}
