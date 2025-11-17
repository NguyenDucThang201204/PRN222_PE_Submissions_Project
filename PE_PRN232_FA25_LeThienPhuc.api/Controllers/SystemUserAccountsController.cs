using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.Models;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PE_PRN232_FA25_LeThienPhuc.api.Controllers
{
    [Route("Accounts")]
    [ApiController]
    public class SystemUserAccountsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly SystemUserAccountService _userAccountsService;

        public SystemUserAccountsController(IConfiguration config, SystemUserAccountService userAccountsService)
        {
            _config = config;
            _userAccountsService = userAccountsService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { errorCode = "HB40001", message = "Email and Password are required" });
            }

            var user = await _userAccountsService.GetUserAccount(request.UserName, request.Password);

            if (user == null)
            {
                return StatusCode(401, new { errorCode = "HB40101", message = "Invalid email or password" });
            }

            var token = GenerateJSONWebToken(user);

            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Invalid role" });

            var response = new LoginResponse(token, user.RoleId);

            return Ok(response);
        }

        private string GenerateJSONWebToken(BearAccount systemAccount)
        {
            if (systemAccount.RoleId != 1 && systemAccount.RoleId != 2 && systemAccount.RoleId != 3 && systemAccount.RoleId != 4)
            {
                return string.Empty;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]
                    {
                    new(ClaimTypes.Name, systemAccount.UserName),
                    //new(ClaimTypes.Email, systemAccount.Email),
                    new(ClaimTypes.Role, systemAccount.RoleId.ToString()),
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public sealed record LoginRequest(string UserName, string Password);
        public sealed record LoginResponse(string Token, int Role);
    }
}
