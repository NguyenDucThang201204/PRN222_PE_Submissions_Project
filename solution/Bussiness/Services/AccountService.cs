using Bussiness.Interfaces;
using Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<ClaimsPrincipal> AuthenticateAsync(string email, string password)
        {
            var account = _accountRepository.GetByEmail(email);
            if (account != null && account.Password == password)
            {
                var roleName = account.RoleId switch
                {
                    1 => "Admin",
                    2 => "Manager",
                    3 => "Staff",
                    4 => "Member",
                    _ => "Unknown"
                };
                var claims = new List<Claim>
                   {
                       new Claim(ClaimTypes.Name, account.Email),
                       new Claim(ClaimTypes.Role, roleName)
                   };
                var identity = new ClaimsIdentity(claims, "CookieAuth");
                return new ClaimsPrincipal(identity);
            }
            return null;
        }
    }
}
