using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PE_PRN232_FA25_VuThanhAn_BE.Controllers
{
	[Route("Accounts")]
	[ApiController]
	public class BearAccountController : ControllerBase
	{
		private readonly IConfiguration _config;
		private readonly IBearAccountService _bearAccountService;

		public BearAccountController(IConfiguration config, IBearAccountService bearAccountService)
		{
			_config = config;
			_bearAccountService = bearAccountService;
		}

		public sealed record LoginRequest(string UserName, string Password);

		[HttpPost("Login")]
		public async Task<ActionResult<ErrorCodeModel>> Login([FromBody] LoginRequest request)
		{
			var user = await _bearAccountService.GetBearAccountAsync(request.UserName, request.Password);

			if (user == null)
				return StatusCode(404, ErrorCodeModel.NotFound());

			var token = GenerateJSONWebToken(user);

			string roleName = user.RoleId switch
			{
				1 => "admin",
				2 => "manager",
				3 => "staff",
				4 => "member",
				_ => "unknown"
			};

			return Ok(new
			{
				token,
				role = user.RoleId,
				roleName
			});
		}

		private string GenerateJSONWebToken(BearAccount bearAccount)
		{
			try
			{
				var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
				var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

				var token = new JwtSecurityToken(_config["Jwt:Issuer"]
						, _config["Jwt:Audience"]
						, new Claim[]
						{
		new(ClaimTypes.Name, bearAccount.UserName),
		new(ClaimTypes.Email, bearAccount.Email),
		new(ClaimTypes.Role, bearAccount.RoleId.ToString()),
						},
						expires: DateTime.Now.AddMinutes(120),
						signingCredentials: credentials
					);

				var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

				return tokenString;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

	}
}
