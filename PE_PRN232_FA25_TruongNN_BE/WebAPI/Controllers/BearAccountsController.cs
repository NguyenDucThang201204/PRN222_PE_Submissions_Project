using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositores.Models;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("Accounts")]
    [ApiController]
    public class BearAccountsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly BearAccountService _userAccountsService;

        public BearAccountsController(IConfiguration config, BearAccountService userAccountsService)
        {
            _config = config;
            _userAccountsService = userAccountsService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userAccountsService.GetUserAccount(request.UserName, request.Password);

            if (user == null)
                return Unauthorized();

            var token = GenerateJSONWebToken(user);

            return Ok(new LoginResponse { Token = token, Role = user.RoleId });
        }

        private string GenerateJSONWebToken(BearAccount BearAccount)
        {
          
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]
                    {
                    new(ClaimTypes.Email, BearAccount.Email),

                    new(ClaimTypes.Role, BearAccount.RoleId.ToString()),
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public sealed record LoginRequest(string UserName, string Password);

        public class LoginResponse
        {
            public string Token { get; set; }
            public int Role { get; set; }
        }
    }
}
