using Repository.Models;
using Repository.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BearAccountService
    {
        private BearAccountRepo bearAccountRepo;

        public BearAccountService(BearAccountRepo bearAccountRepo)
        {
            this.bearAccountRepo = bearAccountRepo;
        }

        public async Task<BearAccount> GetAccountByEmailAndPassword(string email, string password)
        {
            var account = await bearAccountRepo.GetAccountByEmail(email);
            if (account == null || account.Password != password)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            return account;
        }
    }
}
