using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_NguyenNMK_BusinessLogicLayer.Services;
using PRN232_SU25_NguyenNMK_DataAccessLayer.NewFolder;

namespace PRN232_FA25_SE170356.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                var errorCode = ex.Message.StartsWith("B") ? ex.Message.Substring(0, 7) : "B50001";
                var message = ex.Message.StartsWith("B") ? ex.Message.Substring(8) : "Internal server error";
                return StatusCode(errorCode switch
                {
                    "B40101" => 401,
                    "B40301" => 403,
                    _ => 500
                }, new ErrorResponse { ErrorCode = errorCode, Message = message });
            }
        }
    }
}
