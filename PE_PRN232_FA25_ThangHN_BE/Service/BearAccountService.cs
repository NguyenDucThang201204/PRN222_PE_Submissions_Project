using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BearAccountService
    {
        private readonly BearAccountRepository _repo;
        public BearAccountService(BearAccountRepository repo)
        {
            _repo = repo;
        }
        public async Task<BearAccount?> Authenticate(string email, string password) => await _repo.GetAccountAsync(email, password);

        public string GetRoleName(int? roleId)
        {
            return roleId switch
            {
                1 => "Admin",
                2 => "Manager",
                3 => "Staff",
                4 => "Member",
                _ => "unknown",
            };
        }

        public (string token, string role) GenerateJWTToken(BearAccount account, IConfiguration configuration)
        {
            var roleId = account.RoleId ?? 0;
            if (roleId < 1 || roleId > 4)
                throw new AuthenticationException("You have no permission to access this function!");

            var jwtKey = configuration["Jwt:SecretKey"];
            var jwtIssuer = configuration["Jwt:Issuer"];
            var jwtAudience = configuration["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!));
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
