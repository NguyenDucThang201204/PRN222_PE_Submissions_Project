using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class AccountsService
    {
        private readonly AccountsRepository _repo;

        public AccountsService(AccountsRepository repo) => _repo = repo;

        public async Task<BearAccount?> Authenticate(string email, string password) 
            => await _repo.GetUserAccount(email, password);

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

            var jwtKey = configuration["Jwt:Key"];
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
