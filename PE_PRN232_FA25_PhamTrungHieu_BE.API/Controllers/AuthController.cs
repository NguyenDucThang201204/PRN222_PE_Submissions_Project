using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PE_PRN232_FA25_PhamTrungHieu_BE.BAL.Services;
using PE_PRN232_FA25_PhamTrungHieu_BE.DAL.ModelExtensions;

namespace PE_PRN232_FA25_PhamTrungHieu_BE.API.Controllers
{
    [Route("Accounts/Login")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IBearAccountService _service;
        private readonly IConfiguration _config;

        public AuthController(IBearAccountService service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var response = await _service.LoginAsync(request);
            if (string.IsNullOrEmpty(response.Role)) return NotFound(ErrorResponse.NotFound());

            var token = GenerateAccessToken(response.Role);
            response.Token = token;
            return Ok(response);
        }

        private string GenerateAccessToken(string role)
        {
            var secretKey = _config["JwtSettings:SecretKey"];
            var issuer = _config["JwtSettings:Issuer"];
            var audience = _config["JwtSettings:Audience"];

            var claims = new Claim[]
            {
               new(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: issuer, audience: audience, claims: claims, signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
