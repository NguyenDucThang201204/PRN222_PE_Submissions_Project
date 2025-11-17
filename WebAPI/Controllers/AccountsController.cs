using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models;
using WebAPI.Models.DTOs;
using Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly BearAccountService _accountService;

        public AccountsController(IConfiguration config, BearAccountService accountService)
        {
            _config = config;
            _accountService = accountService;
        }

        /// <summary>
        /// Authenticate user and return JWT token with role
        /// </summary>
        /// <param name="request">Login credentials (email and password)</param>
        /// <returns>JWT token and role if successful</returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault();
                return BadRequest(new ErrorResponse("HB40001", firstError?.ErrorMessage ?? "Invalid input"));
            }

            try
            {
                var user = await _accountService.GetUserAccount(request.Email, request.Password);

                if (user == null)
                {
                    return Unauthorized(new ErrorResponse("HB40101", "Invalid email or password"));
                }

                // Check if role is authorized (only roles 1-4 can access: administrator, moderator, developer, member)
                if (!IsAuthorizedRole(user.RoleId))
                {
                    return Unauthorized(new ErrorResponse("HB40101", "User role is not authorized to access the system"));
                }

                var token = GenerateJwtToken(user);
                var roleName = GetRoleName(user.RoleId);

                var response = new LoginResponse
                {
                    Token = token,
                    Role = roleName
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse("HB50001", "Internal server error occurred"));
            }
        }

        /// <summary>
        /// Generate JWT token for authenticated user
        /// </summary>
        private string GenerateJwtToken(BearAccount account)
        {
            // Only generate token for authorized roles (1-4)
            if (!IsAuthorizedRole(account.RoleId))
            {
                return string.Empty;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roleName = GetRoleName(account.RoleId);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, roleName),
                new Claim("AccountId", account.AccountId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        /// <summary>
        /// Get role name from role ID
        /// </summary>
        private string GetRoleName(int? roleId)
        {
            return roleId switch
            {
                1 => "Admin",
                2 => "Manager",
                3 => "Staff",
                4 => "Member",
                _ => "Guest"
            };
        }

        /// <summary>
        /// Check if role is authorized to access the system
        /// Only roles 1-4 (administrator, moderator, developer, member) are allowed
        /// </summary>
        private bool IsAuthorizedRole(int? roleId)
        {
            return roleId >= 1 && roleId <= 4;
        }
    }
}

