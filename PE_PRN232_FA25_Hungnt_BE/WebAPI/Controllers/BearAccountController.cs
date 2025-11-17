using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.Models;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BearAccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly BearAccountService _accountService;

        public BearAccountController(IConfiguration config,BearAccountService userAccountService)
        {
            _config = config;
            _accountService = userAccountService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _accountService.GetAccount(request.Email, request.Password);

            if (user == null || user.Result == null)
                return Unauthorized();

            var token = GenerateJSONWebToken(user.Result);
            return Ok(token);
        }

        private string GenerateJSONWebToken(BearAccount account)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]
                    {
                    new(ClaimTypes.Name, account.UserName),
                    //new(ClaimTypes.Email, systemUserAccount.Email),
                    new(ClaimTypes.Role, account.RoleId.ToString()),
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
        public sealed record LoginRequest(string Email, string Password);
    }
}
