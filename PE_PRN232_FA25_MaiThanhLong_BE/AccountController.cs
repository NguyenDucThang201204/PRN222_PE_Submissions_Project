using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PE_PRN232_FA25_MaiThanhLong_BE
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly AccountService _accountService;

        public AccountController(IConfiguration config, AccountService accountService)
        {
            _config = config;
            _accountService = accountService;
        }

        [HttpPost("/Accounts/Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _accountService.GetUserAccount(request.UserName, request.Password);

            if (user == null)
                return Unauthorized();

            var token = GenerateJSONWebToken(user);

            return Ok(new LoginResponse { Token = token, Role = user.RoleId });
        }

        private string GenerateJSONWebToken(BearAccount account)
        {
            

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]
                    {
                    new(ClaimTypes.Email, account.Email),

                    new(ClaimTypes.Role, account.RoleId.ToString()),
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
