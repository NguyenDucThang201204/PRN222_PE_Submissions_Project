using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PE_PRN232_FA25_NguyenThaiDuy_BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenThaiDuy_BE.Repositories
{
    public class BearAccountRepository : IAccountRepository
    {
        private readonly FA25BearDBContext _context;
        private readonly IConfiguration _configuration;

        public BearAccountRepository(FA25BearDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public AuthResponse Authenticate(string email, string password)
        {
            var account = _context.BearAccounts
                .FirstOrDefault(a => a.Email == email && a.Password == password);

            if (account == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            string roleName = account.RoleId switch
            {
                5 => "administrator",
                6 => "manager",
                7 => "staff",
                4 => "member",
                _ => null
            };

            if (roleName == null)
                throw new UnauthorizedAccessException("Invalid role assignment");

            
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "default_secret_key");
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Email, account.Email),
            new Claim(ClaimTypes.Name, account.FullName),
            new Claim("Role", account.RoleId.ToString()),
            new Claim(ClaimTypes.Role, roleName)
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = tokenHandler.WriteToken(token);

            return new AuthResponse
            {
                Token = jwtToken,
                Role = account.RoleId.ToString()
            };
        }


    }
}
