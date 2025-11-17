using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PE_PRN232_FA25_PhanTuanAn_BE.Repositories.Models;
using PE_PRN232_FA25_PhanTuanAn_BE.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PE_PRN232_FA25_PhanTuanAn_BE.WebAPI.Controllers
{
    [Route("Accounts")]
    [ApiController]
    public class BearAccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly BearAccountService _bearAccountsService;

        public BearAccountController(IConfiguration config, BearAccountService bearAccountsService)
        {
            _config = config;
            _bearAccountsService = bearAccountsService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _bearAccountsService.GetUserAccount(request.Email, request.Password);

            if (user == null || user.Result == null)
                return Unauthorized();

            var token = GenerateJSONWebToken(user.Result);

            return Ok(token);
        }

        private string GenerateJSONWebToken(BearAccount bearUserAccount)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]
                    {
                new(ClaimTypes.Name, bearUserAccount.Email),
                new(ClaimTypes.Role, bearUserAccount.Role.ToString()),
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
