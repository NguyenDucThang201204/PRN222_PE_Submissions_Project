using BO.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Bear.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Accounts : ControllerBase
    {
        private readonly IBearAccountService _bearAccountService;

        public Accounts (IBearAccountService bearAccountService)
        {
            _bearAccountService = bearAccountService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            var result = _bearAccountService.Login(login);
            if (result == null)
            {
                return Unauthorized("Invalid Email or password");
            }

            return Ok(result);
        }
    }
}
