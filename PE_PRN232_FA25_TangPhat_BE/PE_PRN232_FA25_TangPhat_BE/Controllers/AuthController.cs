using BusinessObjects.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace PE_PRN232_FA25_TangPhat_BE.Controllers
{
    [Route("Accounts/Login")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _acc;

        public AuthController(IAccountService acc)
        {
            _acc = acc;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorResponse.Fail(ErrorCode.HB40001, "Invalid Data"));
            }
            var response = await _acc.LoginAsync(request);
            if (response == null)
                return BadRequest(ErrorResponse.Fail(ErrorCode.HB40001, "Invalid username or password"));
            return Ok(response);
        }
    }
}
