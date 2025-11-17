using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using Service;
using Service.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PE_PRN232_FA25_HoangMinhDai_BE.Controllers
{
    [Route("api/Accounts/Login")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;

        public AuthController(IAuthService authService, IConfiguration config)
        {
            _authService = authService;
            _config = config;
        }

        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _authService.Login(model.userName, model.password);
            if (user == null)
            {
                return Unauthorized(new { errorCode = "HB40101", message = "Token missing/invalid" });
            }

            var token = GenerateJwtToken(user);

            return Ok(new LoginResponseModel
            {
                token = token,
                role = user.RoleId
            });
        }

        private string GenerateJwtToken(BearAccount user)
        {
            var claims = new[]
            {
            new Claim("id", user.AccountId.ToString()),
            new Claim("email", user.Email.ToString()),
            new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
