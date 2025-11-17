using Azure.Core;
using BearPetManagement_API.DTOs;
using BearPetManagement_Repository.Models;
using BearPetManagement_Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BearPetManagement_API.Controllers
{
    [Route("Accounts/Login")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserService _service;
        public AuthController(IConfiguration configuration, UserService userService)
        {
            _config = configuration;
            _service = userService;
        }

        // POST api/<AuthController>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _service.GetUser(request.UserName, request.Password);
            if (user == null)
            {
                return Unauthorized(new { Message = "Username hay password không đúng", Token = "" });
            }

            return Ok(GenerateJSONWebToken(user));
        }
        private string GenerateJSONWebToken(BearAccount systemUserAccount)
        {
            //var allowedRoles = new List<int> { 1, 2, 3, 4 };
            //if (!allowedRoles.Contains(roleId))
            //{
            //    return String.Empty;
            //}

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]
                    {
                    new(ClaimTypes.Name, systemUserAccount.UserName),
                    new(ClaimTypes.Email, systemUserAccount.Email),
                    new(ClaimTypes.Role, systemUserAccount.RoleId.ToString()),
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
