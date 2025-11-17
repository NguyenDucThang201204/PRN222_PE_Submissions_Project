using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo;

namespace PE_PRN232_FA25_TranLacDuy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly BearAccountRepo _repo;
        private readonly JWTService _ser;
        public AccountsController(BearAccountRepo repo, JWTService ser)
        {
            _repo=repo;
            _ser=ser;
        }
            [HttpPost("login")]
            public IActionResult GetToken(string username, string password)
            {
                var account = _repo.Login(username, password);
                var token = _ser.GenerateToken(account.AccountId, account.Role, account.Email);
                return Ok(new { token, account.Role});
            }

        }
    
}
