using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_VoHoangTuanKiet_BE.Models;

namespace PE_PRN232_FA25_VoHoangTuanKiet_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserService _userService;

        public AccountsController(UserService userService)
        {
            _userService = userService;
        }

        // POST api/<UserController>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = _userService.Login(model.Email, model.Password);

            if (user == null)
            {
                return BadRequest(new { errorCode = "400", message = "Wrong credential" });
            }
            else
            {
                return Ok(user);
            }
        }
    }
}
