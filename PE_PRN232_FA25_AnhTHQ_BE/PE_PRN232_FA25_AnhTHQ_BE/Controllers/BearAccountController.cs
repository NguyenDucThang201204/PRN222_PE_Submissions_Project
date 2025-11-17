using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_AnhTHQ_BLL.DTOs;
using PE_PRN232_FA25_AnhTHQ_BLL.DTOs.Response;
using PE_PRN232_FA25_AnhTHQ_BLL.Services;

namespace PE_PRN232_FA25_AnhTHQ_BE.Controllers
{
    [Route("/Accounts")]
    [ApiController]
    public class BearAccountController : ControllerBase
    {
        private readonly BearAccountService _accountService;

        public BearAccountController(BearAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _accountService.LoginAsync(request.Email, request.Password);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                    return StatusCode(400, ApiResponse<string>.Error(ApiStatusCode.HB40001, "Invalid email or password"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error(ApiStatusCode.HB50001, ex.Message));
            }
        }
    }
}
