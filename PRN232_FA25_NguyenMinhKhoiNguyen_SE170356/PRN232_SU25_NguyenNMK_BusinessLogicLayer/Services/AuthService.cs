using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PRN232_SU25_NguyenNMK_DataAccessLayer.NewFolder;
using PRN232_SU25_NguyenNMK_DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_NguyenNMK_BusinessLogicLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                Console.WriteLine("Login failed: Email or password is empty");
                throw new Exception("B40101: Invalid credentials");
            }

            var accounts = await _unitOfWork.BearAccountRepository.GetAllAsync();
            var account = accounts.FirstOrDefault(a => a.Email == request.Email && a.Password == request.Password);

            if (account == null)
            {
                Console.WriteLine($"Login failed: No account found for email={request.Email}, password={request.Password}");
                throw new Exception("B40101: Invalid credentials");
            }

            Console.WriteLine($"Account found: Email={account.Email}, Role={account.RoleId}");

            if (account.RoleId < 1 || account.RoleId > 4)
            {
                Console.WriteLine($"Invalid role: {account.RoleId}");
                throw new Exception("B40301: Role not allowed");
            }

            var roleName = account.RoleId switch
            {
                1 => "Manager",
                2 => "Staff",
                3 => "Member",
                4 => "User",
                _ => throw new Exception("B40301: Invalid role")
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            if (string.IsNullOrEmpty(_configuration["Jwt:Key"]))
            {
                Console.WriteLine("JWT Key is not configured in appsettings.json");
                throw new Exception("B50002: JWT Key is not configured");
            }
            if (key.Length < 16)
            {
                Console.WriteLine($"JWT Key length ({key.Length}) is too short, minimum 16 bytes required");
                throw new Exception("B50003: JWT Key too short");
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, account.Email),
                    new Claim(ClaimTypes.Role, roleName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            Console.WriteLine($"Token generated successfully for {account.Email}");
            return new LoginResponse
            {
                Token = tokenHandler.WriteToken(token),
                Role = roleName
            };
        }
    }
}
