using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.Models;

namespace Repositories
{
    public class AccountsRepository : GenericRepository<BearAccount>
    {
        public AccountsRepository(BearManagementDbContext context) : base(context)
        {
        }

        public async Task<BearAccount?> GetUserAccount(string email, string password)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
