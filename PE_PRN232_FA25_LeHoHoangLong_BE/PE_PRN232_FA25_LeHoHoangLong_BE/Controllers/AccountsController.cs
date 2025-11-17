using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_LeHoHoangLong_BE.Middleware;
using Repository.DTOs;
using Service;

namespace PE_PRN232_FA25_LeHoHoangLong_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccSer _accSer;
        public AccountsController(IAccSer accSer)
        {
            _accSer = accSer;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var response = await _accSer.LoginAsync(loginRequest);
            if (response == null)
                return BadRequest(ErrorResponse.FromErrorCode(ErrorCode.HB40001, "Invalid username or password"));
            return Ok(response);
        }
    }
}
