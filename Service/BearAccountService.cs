using Repository;
using Repository.Models;
using Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BearAccountService
    {
        private readonly BearAccountRepository _bearAccountRepository;
        private readonly TokenService _tokenService;
        public BearAccountService(BearAccountRepository repo, TokenService tokenService)
        {
            _bearAccountRepository = repo;
            _tokenService = tokenService;

        }

        public async Task<LoginResponse?> GetUserAccount(string userName, string password)
        {
            var user = await _bearAccountRepository.GetUserAccount(userName, password);
            if (user == null || user.Password != password)
                return null;

            var roleName = Enum.GetName(typeof(Role), user.RoleId) ?? "Unknown";
            var accessToken = _tokenService.GenerateAccessToken(user);

            return new LoginResponse()
            {
                Token = accessToken,
                Role = roleName,
            };
        }
    }
}
