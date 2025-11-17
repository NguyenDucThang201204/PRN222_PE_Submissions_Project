using BLL.IServices;
using DAL.DTOs;
using DAL.Entities;
using DAL.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class LoginService : ILoginService
    {
        private readonly IBearAccountRepository _userRepo;
        private readonly IConfiguration _configuration;

        public LoginService(IBearAccountRepository repo, IConfiguration configuration)
        {
            _userRepo = repo;
            _configuration = configuration;
        }

        private string GenerateJwtToken(BearAccount user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Role, user.RoleId.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(int.Parse(_configuration["Jwt:ExpiryInDays"])),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResponseDTO> LoginFunc(string email, string password)
        {
            var user = await _userRepo.LoginAsync(email, password);

            if (user == null) throw new Exception("Invailde email (as user name) or password");

            var token = GenerateJwtToken(user);

            return new LoginResponseDTO
            {
                Message = "Login successfull",
                Token = token,
                Role = user.RoleId switch
                   {
                        1 => "Admin",
                        2 => "Manager",
                        3 => "Staff",
                        4 => "Members"
                    }

            };
        }

    }
}
