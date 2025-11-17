using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PE_PRN232_FA25_LeCongHung_BLL.DTOs;
using PE_PRN232_FA25_LeCongHung_DAL.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PE_PRN232_FA25_LeCongHung_BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public AccountService(IUnitOfWork unitOfWork, string secretKey, string issuer, string audience)
        {
            _unitOfWork = unitOfWork;
            _secretKey = secretKey;
            _issuer = issuer;
            _audience = audience;
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request)
        {
            // UserName is the Email (as per requirement: "Sử dụng Email (làm tên người dùng) và Password")
            var account = await _unitOfWork.BearAccountRepository.GetAll()
                .FirstOrDefaultAsync(a => a.Email == request.UserName && a.Password == request.Password);

            if (account == null) return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, account.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, account.FullName ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                new Claim("RoleId", account.RoleId?.ToString() ?? "0")
            };

            // Add role claim based on RoleId
            if (account.RoleId == 1)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Manager"));
            }
            else if (account.RoleId == 2)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Staff"));
            }
            else if (account.RoleId == 3)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Member"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponseDTO
            {
                Token = tokenString,
                Email = account.Email ?? string.Empty,
                FullName = account.FullName,
                RoleId = account.RoleId
            };
        }
    }
}

