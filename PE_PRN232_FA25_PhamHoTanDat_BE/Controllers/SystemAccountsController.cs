using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repos.Models;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PE_PRN232_FA25_PhamHoTanDat_BE.Controllers
{
    [Route("Accounts/Login")]
    [ApiController]
    public class SystemAccountsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly SystemAccountService _userAccountsService; //// don't forget add Dependency Injection in program.cs

        public SystemAccountsController(IConfiguration config, SystemAccountService userAccountsService)
        {
            _config = config;
            _userAccountsService = userAccountsService;     //// Add DJ
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.userName) || string.IsNullOrEmpty(request.password))
            {
                return BadRequest(new { errorCode = "HB40001", message = "Email and Password are required" });
            }

            var user = _userAccountsService.GetUserAccount(request.userName, request.password);

            if (user == null || user.Result == null)
            {
                return Unauthorized(new { errorCode = "HB40101", message = "Invalid email or password" });
            }

            var token = GenerateJSONWebToken(user.Result);

            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Invalid role" });

            //Map numeric role to string
            string roleString = user.Result.RoleId switch
            {

                1 => "administrator",
                2 => "manager",
                3 => "staff",
                4 => "member",
                _ => "unknown"
            };

            return Ok(new
            {
                token,
                role = roleString,
            });
        }

        private string GenerateJSONWebToken(BearAccount systemUserAccount)
        {
            //if (systemUserAccount.RoleId != 2 && systemUserAccount.RoleId != 3)
            //{
            //    return string.Empty;
            //}

            if (systemUserAccount.RoleId != 1 && systemUserAccount.RoleId != 2 && systemUserAccount.RoleId != 3 && systemUserAccount.RoleId != 4)
            {
                return string.Empty;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]
                    {
                    new(ClaimTypes.Name, systemUserAccount.Username),
                    //new(ClaimTypes.Email, systemUserAccount.Email),
                    new(ClaimTypes.Role, systemUserAccount.RoleId.ToString()),
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public sealed record LoginRequest(string userName, string password);
    }
}
