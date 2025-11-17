using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_DaoTrongTien_BE.API.DTOs;
using PE_PRN232_FA25_DaoTrongTien_BE.BLL.Services;

namespace PE_PRN232_FA25_DaoTrongTien_BE.API.Controllers
{

        [Route("Accounts/")]
        [ApiController]
        public class BearAccountController : ControllerBase
        {
            private IBearAccountService _service;
            public BearAccountController(IBearAccountService service)
            {
                _service = service;
            }

            [HttpPost("Login")]
            public async Task<IActionResult> Login(LoginRequest loginRequest)
            {
                var result = await _service.GetBearAccount(loginRequest.userName, loginRequest.password);
                if (result == null)
                {
                    return Unauthorized(new LoginResponse { token = result });

                }
                return Ok(new LoginResponse
                {
                    token = result
                });
            }

        }
}
