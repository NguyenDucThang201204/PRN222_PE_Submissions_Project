using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.ThienNH.Models;
using Services.ThienNH;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.ThienNH.Controllers
{
    [Route("api/Accounts")]
    [ApiController]
    public class BearAccountsController : Controller
    {
        private readonly IConfiguration _config;
        private readonly BearAccountService _userAccountsService;

        public BearAccountsController(IConfiguration config, BearAccountService userAccountsService)
        {
            _config = config;
            _userAccountsService = userAccountsService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userAccountsService.GetUserAccount(request.Email, request.Password);

            if (user == null || user.Result == null)
                return Unauthorized();

            var token = GenerateJSONWebToken(user.Result);

            return Ok(token);
        }

        private LoginResponse GenerateJSONWebToken(BearAccount systemAccount)
        {

            if (systemAccount.RoleId == 1 || systemAccount.RoleId == 5 || systemAccount.RoleId == 6 || systemAccount.RoleId == 7)
            {
                return new LoginResponse(string.Empty, string.Empty);
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]
                    {
                 new(ClaimTypes.Name, systemAccount.Email),
                 //new(ClaimTypes.Email, systemUserAccount.Email),
                 new(ClaimTypes.Role, systemAccount.RoleId.ToString()),
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponse(tokenString, systemAccount.RoleId.ToString());
        }

        public sealed record LoginRequest(string Email, string Password);
        public sealed record LoginResponse(string Token, string Role);
    }
}
