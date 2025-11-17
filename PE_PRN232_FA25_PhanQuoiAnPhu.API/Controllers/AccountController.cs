using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.Models;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PE_PRN232_FA25_PhanQuoiAnPhu.API.Controllers
{
    [Route("Accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAccountService _service;
        private readonly string ERROR_400 = "Missing/invalid input";
        private readonly string ERROR_401 = "Token missing/invalid";
        private readonly string ERROR_404 = "Resource not found";

        public AccountController(IConfiguration config, IAccountService service)
        {
            _config = config;
            _service = service;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ERROR_400); //400
            //}
            var user = await _service.GetAccount(request.Email, request.Password);
            System.Diagnostics.Debug.WriteLine(user);

            if (user == null)
                return Unauthorized(ERROR_404); //401

            if (user.RoleId != 1 && user.RoleId != 2 && user.RoleId != 3 && user.RoleId != 4)
                return Unauthorized(ERROR_401); //401

            var token = GenerateJSONWebToken(user);

            return Ok(new { token, user.RoleId });
        }

        private string GenerateJSONWebToken(BearAccount account)
        {

            if (account.RoleId != 1 && account.RoleId != 2 && account.RoleId != 3 && account.RoleId != 4)
            {
                return string.Empty;
            }

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
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public sealed record LoginRequest(string Email, string Password);
    }
}
