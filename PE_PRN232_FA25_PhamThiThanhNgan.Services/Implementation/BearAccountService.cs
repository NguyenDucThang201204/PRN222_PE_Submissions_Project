using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Interface;
using PE_PRN232_FA25_PhamThiThanhNgan.Services;
using Practice_FA25_PE_Services.DTO;
using Practice_FA25_PE_Services.Interface;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Practice_FA25_PE_Services.Implementation
{
    public class BearAccountService : IBearAccountService
    {
        private readonly IBearAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;

        public BearAccountService(IBearAccountRepository accountRepository, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDTO?> Login(LoginRequestDTO loginRequest)
        {
            var account = await _accountRepository.GetAccountByEmailAndPasswordAsync(
                loginRequest.UserName,
                loginRequest.Password
            );

            if (account == null)
            {
                return null;
            }

            if (!System.Enum.IsDefined(typeof(Role), account.RoleId))
            {
                return null;
            }

            var roleName = RoleExtensions.GetRoleNameById(account.RoleId);

            if (string.IsNullOrEmpty(roleName))
            {
                return null;
            }

            var token = GenerateJwtToken(account.Email, roleName);

            return new LoginResponseDTO
            {
                Token = token,
                Role = account.RoleId.ToString()  
            };
        }

        private string GenerateJwtToken(string email, string roleName)
        {
            var securityKey = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            if (string.IsNullOrEmpty(securityKey) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                throw new InvalidOperationException("JWT configuration is missing in appsettings.json.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, roleName)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}