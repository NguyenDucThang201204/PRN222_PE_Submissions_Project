using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using PE_PRN232_FA25_DinhLeThinh_API.Middleware;
using Service;

namespace PE_PRN232_FA25_DinhLeThinh_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _acc;

        public AccountsController(IAccountService acc)
        {
            _acc = acc;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var response = await _acc.LoginAsync(request);
            if (response == null)
                return BadRequest(ErrorResponse.FromErrorCode(ErrorCode.HB40001, "Invalid username or password"));
            return Ok(response);
        }
    }
}
