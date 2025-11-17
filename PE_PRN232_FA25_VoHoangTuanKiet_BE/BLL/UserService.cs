using DAL;
using DAL.Repo;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserService
    {
        private readonly UserRepo _repo;
        private readonly IConfiguration _configuration;

        public UserService(UserRepo repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }

        public object? Login(string email, string password)
        {
            var user = _repo.Login(email);

            if (user == null || user.Password != password)
            {
                return null;
            }

            if (user.RoleId < 1 || user.RoleId > 4)
            {
                return null;
            }

            return new
            {
                token = GenerateJwtToken(user),
                role = user.GetRoleName(),
            };
        }

        private string GenerateJwtToken(BearAccount user)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? "DefaultKeyForDevelopmentPurposesOnly12345678901234";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, user.GetRoleName()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? "exeapi",
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
