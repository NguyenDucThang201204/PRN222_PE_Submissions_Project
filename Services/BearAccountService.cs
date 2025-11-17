using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BearAccountService
    {
        private readonly BearAccountRepository _repo;

        public BearAccountService() => _repo = new();

        public async Task<BearAccount?> Authenticate(string email, string password) => await _repo.GetUserAccount(email, password);

        public string GetRoleName(int? roleId)
        {
            return roleId switch
            {
                1 => "administrator",
                2 => "moderator",
                3 => "developer",
                4 => "member",
                _ => "unknown",
            };
        }

        public (string token, string role) GenerateJWTToken(BearAccount account, IConfiguration configuration)
        {
            var roleId = account.RoleId ?? 0;
            if (roleId < 1 || roleId > 4)
                throw new AuthenticationException("You have no permission to access this function!");

            var jwtKey = configuration["Jwt:Key"];
            var jwtIssuer = configuration["Jwt:Issuer"];
            var jwtAudience = configuration["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Email),
                new Claim(ClaimTypes.Role, account.RoleId!.Value.ToString()),
            };
            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
            );
            return (
                new JwtSecurityTokenHandler().WriteToken(token),
                GetRoleName(account.RoleId)
                );
        }
    }
}
