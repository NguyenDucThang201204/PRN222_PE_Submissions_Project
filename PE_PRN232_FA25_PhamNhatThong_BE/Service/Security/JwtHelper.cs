using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Security
{
    public class JwtHelper(IConfiguration config)
    {
        public static readonly HashSet<string> AllowedRoles = ["manager", "staff", "member"];

        private readonly IConfiguration _config = config;

        public string GenerateJwtToken(BearAccount user, string roleName)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim("role", roleName),
            new Claim("userId", user.AccountId.ToString())
        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "supersecretkey123"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"] ?? "api",
                audience: _config["Jwt:Audience"] ?? "api",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool IsAllowedRole(string? roleName) => AllowedRoles.Contains(roleName);
    }
}
