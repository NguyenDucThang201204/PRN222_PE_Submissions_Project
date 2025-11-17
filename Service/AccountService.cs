using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AccountService
    {
        private readonly AccountRepo _accountRepo;
        public AccountService()
            => _accountRepo = new AccountRepo();
        public async Task<BearAccount> GetUserAccount(string email, string password)
        {
            return await _accountRepo.GetUserAccount(email, password);
        }
    }
}
