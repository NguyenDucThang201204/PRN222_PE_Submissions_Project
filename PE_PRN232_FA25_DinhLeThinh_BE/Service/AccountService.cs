using Model;
using Model.DTOs;
using Repository;

namespace Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accRepo;
        private readonly IJWTService _jwt;

        public AccountService(IAccountRepo accRepo, IJWTService jwt)
        {
            _accRepo = accRepo;
            _jwt = jwt;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var acc = await _accRepo.LoginAsync(request.Email, request.Password);

            if (acc == null)
            {
                return null;
            }

            string token = _jwt.GenerateToken(acc.FullName, acc.Email, acc.RoleId);

            var response = new LoginResponse()
            {
                Token = token,
                Role = GetRoleString(acc.RoleId),
            };

            return response;
        }

        private static string GetRoleString(int? role)
        {
            return role switch
            {
                1 => "Admin",
                2 => "Manager",
                3 => "Staff",
                4 => "Member",
                _ => "Guest"
            };
        }
    }
}
