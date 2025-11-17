using Microsoft.EntityFrameworkCore;
using Repositories.ThienNH.Basic;
using Repositories.ThienNH.DBContext;
using Repositories.ThienNH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ThienNH
{
    public class BearAccountRepository : GenericRepository<BearAccount>
    {
        public BearAccountRepository() { }
        public BearAccountRepository(FA25BearDBContext context) => _context = context;

        public async Task<BearAccount> GetUserAccount(string email, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
