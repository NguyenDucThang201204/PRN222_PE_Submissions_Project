using Microsoft.IdentityModel.Tokens;
using PE_PRN232_FA25_AnhTHQ_DAL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_AnhTHQ_BLL.Services
{
    public class JwtService
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _key;
        private readonly int _minutes;

        public JwtService(string issuer, string audience, string key, int accessTokenMinutes)
        {
            _issuer = issuer;
            _audience = audience;
            _key = key;
            _minutes = accessTokenMinutes;
        }

        public string GenerateToken(BearAccount account)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, (account.RoleId).ToString()),
                new Claim("AccountId", account.AccountId.ToString()),
                new Claim("Username", account.UserName),
                new Claim("Email", account.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_minutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
