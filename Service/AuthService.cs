using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Repository.DTO;

namespace Service
{
    public class AuthService
    {
        private readonly Fa25bearDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(Fa25bearDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<LoginResponse?> LoginAsync(string email, string password)
        {
            var account = await _context.BearAccounts.FirstOrDefaultAsync(a =>
                a.Email == email && a.Password == password);



            if (account == null || account.RoleId == null) return null;

            var roleName = GetRoleName(account.RoleId);


            if (string.IsNullOrEmpty(roleName)) return null;

            var token = GenerateJwtToken(account.Email, roleName);
            return new LoginResponse { Token = token, Role = roleName };
        }


        private string GenerateJwtToken(string email, string role)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string? GetRoleName(int? roleId)
        {
            return roleId switch
            {
                1 => "Admin",
                2 => "Manager",
                3 => "Staff",
                4 => "Member",
                _ => "unknown"
            };
        }

    }
}
