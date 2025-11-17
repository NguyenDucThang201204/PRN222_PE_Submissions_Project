using BusinessObjects.DTOs;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _acc;
        private readonly IJWTService _jwt;
        public AccountService(IAccountRepo acc, IJWTService jwt)
        {
            _acc = acc;
            _jwt = jwt;
        }
        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var acc = await _acc.LoginAsync(request.UserName, request.Password);

            if (acc == null)
            {
                return null;
            }

            string token = _jwt.GenerateToken(acc.UserName, acc.Email, acc.RoleId);

            var response = new LoginResponse()
            {
                token = token,
                role = GetRoleString(acc.RoleId),
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
