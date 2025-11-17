using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using Repository.Repository.Interfaces;
using Repository.Requests;
using Repository.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthService: IAuthService
    {
        private readonly IBearAccountRepository _accountRepo;
        private readonly IConfiguration _config;

        public AuthService(IBearAccountRepository accountRepo, IConfiguration config)
        {
            _accountRepo = accountRepo;
            _config = config;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var account = await _accountRepo.GetByEmailAsync(request.Email);
            if (account == null || account.Password != request.Password)
                throw new UnauthorizedAccessException("Invalid email or password");

            var token = GenerateJwtToken(account);
            var roleName = MapRole(account.RoleId);
            return new LoginResponse
            {
                Token = token,
                Role = roleName,
            };
        }

        private string GenerateJwtToken(BearAccount account)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            string roleName = MapRole(account.RoleId);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, roleName)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string MapRole(int? role)
        {
            return role switch
            {
                1 => "Admin",
                2 => "Manager",
                3 => "Staff",
                4 => "Member",
                _ => "Member"
            };
        }
    }
}
