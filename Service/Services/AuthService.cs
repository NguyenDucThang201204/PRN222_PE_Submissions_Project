using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using Repository.Repositories;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthEntity = Repository.Models.BearAccount;

namespace Service.Services;
public class AuthService
{


    private readonly IGenericRepository<AuthEntity> _userRepository;
    private readonly IConfiguration _config;

    public AuthService(IGenericRepository<AuthEntity> userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _config = configuration;
    }

    public async Task<LoginResponse?> Login(string username, string password)
    {
        var users = await _userRepository.GetAsync(u => u.Email == username && u.Password == password);
        var user = users.FirstOrDefault();
        if (user == null )
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }
        else
        {
            return new LoginResponse
            {
                Token = GenerateJSONWebToken(user),
                Role = user.RoleId.ToString()
            };
        }   
    }

    private string GenerateJSONWebToken(AuthEntity authEntity)
    {
        if (authEntity.RoleId != 1 && authEntity.RoleId != 2 && authEntity.RoleId != 3 && authEntity.RoleId != 4)
        {
            return string.Empty;
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                , _config["Jwt:Audience"]
                , new Claim[]
                {
                    new(ClaimTypes.Name, authEntity.Email),
                    new(ClaimTypes.Role, authEntity.RoleId.ToString()),
                },
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }


}
