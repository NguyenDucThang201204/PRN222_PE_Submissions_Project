using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.Models;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PE_PRN232_FA25_HoaKhanhHung_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly BearAccountService _service;

        public AccountsController(IConfiguration config, BearAccountService service)
        {
            _config = config;
            _service = service;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            string roleName = "";
            var user = await _service.LogIn(request.userName, request.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = GenerateJSONWebToken(user);

            switch (user.RoleId)
            {
                case 1:
                    roleName = "Admin";
                    break;
                case 2:
                    roleName = "Manager";
                    break;
                case 3:
                    roleName = "Staff";
                    break;
                case 4:
                    roleName = "Member";
                    break;
            }

            return Ok(new
            {
                token = token,
                role = roleName
            });
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
                new(ClaimTypes.Email, account.Email),
                new(ClaimTypes.Role, account.RoleId.ToString()),
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public sealed record LoginRequest(string userName, string Password);
    }
}
