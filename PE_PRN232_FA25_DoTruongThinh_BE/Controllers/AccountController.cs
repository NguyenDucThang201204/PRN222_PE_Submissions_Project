using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repo.DTO;
using Repo.Models;
using Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PE_PRN232_FA25_DoTruongThinh_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly BearAccountService _service;
        private readonly IConfiguration _configuration;



        public AccountController(BearAccountService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("/Accounts/Login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse("400", "Email and password are required"));
            }

            var account = await _service.GetSystemUserAccountAsync(request.Username, request.Password);
            if (account == null)
            {
                return Unauthorized(new ErrorResponse("401", "Invalid email or password"));
            }

            var roleName = ConvertRoleToString(account.RoleId);
            if (roleName == "guest")
            {
                return StatusCode(403, new ErrorResponse("403", "Permission denied"));
            }

            var jwtToken = GenerateJwtToken(account, roleName);
            return Ok(new { token = jwtToken, role = account.RoleId });
        }
        private string ConvertRoleToString(int? role)
        {
            return role switch
            {
                1 => "Admin",
                2 => "Manager",
                3 => "Staff",
                4 => "Member",
                _ => "Guest",
            };
        }
        private string GenerateJwtToken(BearAccount account, string roleName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, roleName),
                new Claim("AccountId", account.AccountId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
