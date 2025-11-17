using BearPetManagement_Repositories;
using BearPetManagement_Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearPetManagement_Services
{
    public class BearAccountService
    {
        private readonly BearAccountRepository _bearAccountRepository;
        private readonly JwtService _jwtService;

        public BearAccountService(BearAccountRepository bearAccountRepository, JwtService jwtService)
        {
            _bearAccountRepository = bearAccountRepository;
            _jwtService = jwtService;
        }

        public async Task<BearAccount> GetAccountAsync(string email, string password)
        {
            var account = await _bearAccountRepository.GetAccountAsync(email, password);
            if (account == null) return null;
            return account;
        }

        public async Task<string> GenerateTokenAsync(BearAccount account)
        {
            return await Task.Run(() => _jwtService.GenerateToken(account));
        }
    }
}
