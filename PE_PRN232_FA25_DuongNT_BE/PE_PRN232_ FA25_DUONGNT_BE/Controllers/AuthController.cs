using BAL.Sevices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PE_PRN232__FA25_DUONGNT_BE.RequestModels;

namespace PE_PRN232__FA25_DUONGNT_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserSevice _service;

        public AccountController(UserSevice service)
        {
            _service = service;
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                // Kiểm tra input
                if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                {
                    return BadRequest(new ApiErrorResponse
                    {
                        ErrorCode = "HB40001",
                        Status = 400,
                        Message = "Missing/invalid input"
                    });
                }

                var (token, role) = await _service.LoginAsync(request.Email, request.Password);

                // Token không được cấp => invalid credentials
                if (token == null)
                    return Unauthorized(new ApiErrorResponse
                    {
                        ErrorCode = "HB40101",
                        Status = 401,
                        Message = "Token missing/invalid"
                    });

                return Ok(new { token, role });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiErrorResponse
                {
                    ErrorCode = "HB50001",
                    Status = 500,
                    Message = "Internal server error"
                });
            }
        }

    }
}
