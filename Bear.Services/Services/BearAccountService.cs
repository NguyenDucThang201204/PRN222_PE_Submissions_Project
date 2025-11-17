using Bear.Repositories.Interface;
using Bear.Repositories.Models;
using Bear.Services.DTO;
using Bear.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bear.Services.Services
{
    public class BearAccountService : IBearAccountService
    {
        private readonly IBearAccountRepository _repo;
        private readonly IConfiguration _config;

        public BearAccountService(IBearAccountRepository repo,
            IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }
        public async Task<BearAccountDTO> GetAccountByEmail(string email, string password)
        {
            var account = await _repo.GetAccountByEmail(email, password);
            string role = null;
            switch (account.RoleId)
            {
                case 1:
                    role = "Admin";
                    break;
                case 2:
                    role = "Manager";
                    break;
                case 3:
                    role = "Staff";
                    break;
                case 4:
                    role = "Member";
                    break;

            }
            var response = new BearAccountDTO
            {
                token = GenerateJSONWebToken(account),
                role = role
            };
            return response;
        }
        private string GenerateJSONWebToken(BearAccount bearAccount)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]
                    {
                    new(ClaimTypes.Name, bearAccount.Username),
                    //new(ClaimTypes.Email, systemUserAccount.Email),
                    new(ClaimTypes.Role, bearAccount.RoleId.ToString()),
                    },
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
