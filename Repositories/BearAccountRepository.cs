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
        private readonly FA25BearDBContext _context;

        public BearAccountRepository() => _context ??= new FA25BearDBContext();

        public async Task<BearAccount?> LogIn(string email, string password)
        {
            var user = await _context.BearAccounts
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return user;
        }
    }
}
