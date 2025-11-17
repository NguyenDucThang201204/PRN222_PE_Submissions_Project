using PE_PRN232_FA25_AnhTHQ_BLL.DTOs.Response;
using PE_PRN232_FA25_AnhTHQ_DAL.Repostitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_AnhTHQ_BLL.Services
{
    public class BearAccountService
    {
        private readonly BearAccountRepos _accountRepos;
        private readonly JwtService _jwtService;
        public BearAccountService(BearAccountRepos accountRepos, JwtService jwtService)
        {
            _accountRepos = accountRepos;
            _jwtService = jwtService;
        }

        public async Task<LoginResponse?> LoginAsync(string email, string password)
        {
            var account = _accountRepos.Login(email, password);
            if (account != null)
            {
                var token = _jwtService.GenerateToken(account);
                return new LoginResponse
                {
                    token = token,
                    role = account.RoleId
                };
            }
            return null;
        }
    }
}
