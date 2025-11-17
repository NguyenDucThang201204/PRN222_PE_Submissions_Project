using BLL.IServices;
using DAL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace PE_PRN232_FA25_NguyenAnh_BE.Controllers
{
    [Route("accounts/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;

        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                var response = await _service.LoginFunc(login.UserName, login.Password);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }

    }
}
