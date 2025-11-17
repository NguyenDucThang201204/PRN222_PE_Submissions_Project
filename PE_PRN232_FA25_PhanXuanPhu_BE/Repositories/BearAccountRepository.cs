using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BearAccountRepository : GenericRepository<BearAccount>
    {
        public BearAccountRepository()
        {
        }
        public BearAccountRepository(FA25BearDBContext context)
        {
            context = _context;
        }

        public async Task<BearAccount> LoginAsync(string email, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

        }
    }
}
