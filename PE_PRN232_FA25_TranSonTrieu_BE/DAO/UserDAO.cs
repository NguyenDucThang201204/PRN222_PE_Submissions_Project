using DAO.DTO;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class UserDAO
    {
        private readonly Fa25bearDbContext _context;
        private readonly IConfiguration _configuration;

        public UserDAO(Fa25bearDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<LoginResponse> Login(DTO.LoginRequest loginRequest)
        {
            // 1. Find user by email
            var user = await _context.BearAccounts
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == loginRequest.Username);

            if (user == null || user.Password != loginRequest.Password)
            {
                throw new Exception("Invalid username or password");
            }
            UserRole roleEnum = (UserRole)(user.RoleId ?? 4);
            if (!Enum.IsDefined(typeof(UserRole), roleEnum))
            {
                roleEnum = UserRole.Member;
            }
            string roleName = roleEnum.ToString();

            var token = GenerateJwtToken(user, roleName);

            return new LoginResponse
            {
                token = token,
                Role = roleName,
            };
        }
        private string GenerateJwtToken(BearAccount user, string roleName)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.AccountId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("name", user.Username),
                new Claim(ClaimTypes.Role, roleName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
public enum UserRole
{
    Admin = 1,
    Manager = 2,
    Staff = 3,
    Member = 4
}

