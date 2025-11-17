using Bear.Services.DTO;
using Bear.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PE_PRN232_FA25_PhamDuyDoanh_BE.api.Controllers
{
    [Route("Accounts/Login")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IBearAccountService _bearAccountService;
        public AuthController(IBearAccountService bearAccountService)
        {
            _bearAccountService = bearAccountService;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            var account = await _bearAccountService.GetAccountByEmail(request.userName, request.password);

            if (account == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(new
            {
                token = account.token,
                role = account.role
            });
        }
    }
}
