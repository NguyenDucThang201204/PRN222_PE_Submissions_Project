using Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repos
{
    public class BearAccountRepo : GenericRepository<BearAccount>
    {
        public BearAccountRepo() : base() { }

        public Task<BearAccount> GetAccountByEmail(string email)
        {
            var account = _context.BearAccounts
                 .Where(acc => acc.Email == email)
                 .FirstOrDefaultAsync();
            return account;
        }
    }
}
