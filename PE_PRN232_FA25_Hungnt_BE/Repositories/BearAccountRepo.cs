using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.DBContext;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BearAccountRepo : GenericRepository<BearAccount>
    {
        public BearAccountRepo() { }
        public BearAccountRepo(Fa25bearDbContext context) => _context = context;

        public async Task<BearAccount> GetUserAccount(string email, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

        }
    }
}
