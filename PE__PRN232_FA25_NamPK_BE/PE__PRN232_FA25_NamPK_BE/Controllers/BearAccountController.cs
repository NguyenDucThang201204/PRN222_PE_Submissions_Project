using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PE__PRN232_FA25_NamPK_BLL;
using PE__PRN232_FA25_NamPK_BLL.DTO;
using PE__PRN232_FA25_NamPK_DAL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PE__PRN232_FA25_NamPK_BE.Controllers
{
    [ApiController]
    [Route("Accounts")]
    public class BearAccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly BearAccountService _bearAccountService;

        public BearAccountController(BearAccountService bearAccountService, IConfiguration config)
        {
            _bearAccountService = bearAccountService;
            _config = config;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthRequest authenRequest)
        {
            var existAccount = await _bearAccountService.Authenticate(authenRequest);
            if (existAccount == null)
                return Unauthorized("Invalid email or password");

            var result = GenerateJSONWebToken(existAccount);
            return Ok(result);
        }

        private AuthenResponse GenerateJSONWebToken(BearAccount existAccount)
        {
            // Nếu RoleId = 4 thì không sinh token
            if (existAccount.Role == 4)
            {
                return new AuthenResponse
                {
                    Token = string.Empty
                };
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim("userId", existAccount.AccountId.ToString()),
            new Claim(ClaimTypes.Email, existAccount.Email),
            new Claim(ClaimTypes.Role, existAccount.Role.ToString()) // 1,2,3,4
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: creds
            );

            return new AuthenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };

        }
    }
}
