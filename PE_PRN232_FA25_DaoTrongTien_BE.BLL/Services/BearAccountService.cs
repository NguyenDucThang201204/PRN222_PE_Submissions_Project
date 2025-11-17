using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PE_PRN232_FA25_DaoTrongTien_BE.DAL.Models;
using PE_PRN232_FA25_DaoTrongTien_BE.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_DaoTrongTien_BE.BLL.Services
{
    public class BearAccountService:IBearAccountService
    {

        private readonly IBearAccountRepository _repo;
        private readonly IConfiguration _config;
        public BearAccountService(IBearAccountRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }
        public async Task<string> GetBearAccount(string email, string password)
        {
            var account = await _repo.GetBearAccount(email, password);
            string token = null;
            if (account != null)
            {
                string accountRole;
                if (account.RoleId == 1)
                {
                    token = GenerateJSONWebToken(account);
                }
                else if (account.RoleId == 2)
                {
                    token = GenerateJSONWebToken(account);
                }
                else if (account.RoleId == 3)
                {
                    token = GenerateJSONWebToken(account);
                }
                else if (account.RoleId == 4)
                {
                    token = GenerateJSONWebToken(account);
                }
                else
                {
                    accountRole = null;
                }
                return token;
            }
            else
            {
                return null;
            }

        }

        public string GenerateJSONWebToken(BearAccount bearAccount)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]
                    {
                    new(ClaimTypes.Email, bearAccount.Email),
                    new(ClaimTypes.Role, bearAccount.RoleId.ToString()),
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

    }
}
