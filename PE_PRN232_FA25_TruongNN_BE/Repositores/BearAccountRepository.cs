using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositores.AppDBContext;
using Repositores.Models;
using Repositories.Basic;

namespace Repositores
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
