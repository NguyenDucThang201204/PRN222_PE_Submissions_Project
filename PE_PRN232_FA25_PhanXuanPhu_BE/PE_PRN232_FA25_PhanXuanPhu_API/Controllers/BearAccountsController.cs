using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PE_PRN232_FA25_PhanXuanPhu_API.Models;
using Repositories.Models;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PE_PRN232_FA25_PhanXuanPhu_API.Controllers
{
    [Route("api/Accounts")]
    [ApiController]
    public class BearAccountsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly BearAccountService _service;

        public BearAccountsController(IConfiguration config, BearAccountService service)
        {
            _config = config;
            _service = service;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                return ErrorHelper.BadRequest("email is required");
            if (string.IsNullOrWhiteSpace(request.Password))
                return ErrorHelper.BadRequest("password is required");

            var user = await _service.LoginAsync(request.Email, request.Password);

            if (user == null)
                return ErrorHelper.Unauthorized();

            var allowedRoles = new List<int> { 1, 2, 3, 4 };
            if (!allowedRoles.Contains(user.RoleId))
            {
                return ErrorHelper.Forbidden();
            }

            var token = GenerateJSONWebToken(user);

            return Ok(new { token = token, role = user.RoleId });
        }

        private string GenerateJSONWebToken(BearAccount bearAccount)
        {
            //if (systemUserAccount.AccountId == 3)
            //{
            //    return string.Empty;
            //}

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                                             _config["Jwt:Audience"],
                                             new Claim[]
                                             {
                                             new(ClaimTypes.Name, bearAccount.UserName),
                                             new(ClaimTypes.Role, bearAccount.RoleId.ToString()),
                                             },
                                             expires: DateTime.Now.AddDays(12),
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public sealed record LoginRequest(string Email, string Password);
    }
}
