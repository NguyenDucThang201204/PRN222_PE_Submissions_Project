using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.Models;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PE_PRN232_FA25_LeQuangThaiSon_BE.Controllers
{
    [Route("api/Accounts/")]
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

        private string GenerateJSONWebToken(BearAccount bearAccount)
        {
            if (bearAccount.RoleId == 1)
            {
                return string.Empty;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]
                    {
                    new(ClaimTypes.Email, bearAccount.Email),

                    new(ClaimTypes.Role, bearAccount.RoleId.ToString()),
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
