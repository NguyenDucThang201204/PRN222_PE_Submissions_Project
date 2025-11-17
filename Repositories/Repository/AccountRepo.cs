using Microsoft.EntityFrameworkCore;
using Repositories.Base;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class AccountRepo : GenericRepository<BearAccount>
    {
        public async Task<BearAccount?> GetOne(string email, string password)
        {
            return await _context.BearAccounts
                .Where(acc => acc.Email == email && acc.Password == password)
                .FirstOrDefaultAsync();
        }
    }
}
