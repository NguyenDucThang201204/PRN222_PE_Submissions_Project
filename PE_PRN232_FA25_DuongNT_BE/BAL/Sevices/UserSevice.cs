using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Sevices
{
    public class UserSevice
    {
        private readonly UserRepository _repository;
        private readonly IConfiguration _config;

        public UserSevice(UserRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task<(string? Token, string? Role)> LoginAsync(string email, string password)
        {
            var account = await _repository.GetByEmailAndPasswordAsync(email, password);
            if (account == null) return (null, null);

            
            string roleName = account.RoleId switch
            {
               
                1 => "Admin",
                2 => "Manager",
                3 => "Staff",
                4 => "Member",
                5 => "administrator",
                6 => "moderator",
                7 => "moderator",

                _ => "unknown"
            };

            // role khác 1-4 thì không cấp token
            if (roleName == "unknown") return (null, null);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, account.Email),
                    new Claim(ClaimTypes.Role, roleName),
                    new Claim("AccountId", account.AccountId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token), roleName);
        }
    }
}
