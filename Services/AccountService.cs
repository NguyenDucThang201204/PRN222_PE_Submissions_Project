using Repositories.Dto;
using Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService
    {
        private readonly AccountRepo _accountRepo;
        private readonly TokenProvider _tokenProvider;

        public AccountService(AccountRepo accountRepo, TokenProvider tokenProvider)
        {
            _accountRepo = accountRepo;
            _tokenProvider = tokenProvider;
        }

        public async Task<LoginResponse> Authenticate(LoginRequest request)
        {
            var account = await _accountRepo.GetOne(request.userName, request.password);
            if (account == null)
            {
                return null;
            }

            var roles = new List<string>();

            if (account.RoleId != 0)
            {
                roles.Add(account.RoleId.ToString());
            }


            // Generate JWT token using the existing JWT service
            var token = _tokenProvider.GenerateToken(
                account.AccountId.ToString(),
                account.Email ?? string.Empty,
                roles
            );

            // Generate refresh token
            var refreshToken = _tokenProvider.GenerateRefreshToken();

            return new LoginResponse
            {
                Token = token,
                Role = account.RoleId != 0 ? account.RoleId.ToString() : "0"
            };
        }
    }
}
