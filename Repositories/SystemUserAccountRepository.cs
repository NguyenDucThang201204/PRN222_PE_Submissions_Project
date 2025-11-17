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
    public class SystemUserAccountRepository : GenericRepository<BearAccount>
    {
        public SystemUserAccountRepository() => _context ??= new DBContext.SU25BearDBContext();

        public SystemUserAccountRepository(DBContext.SU25BearDBContext context) => _context = context;

        public async Task<BearAccount> GetUserAccountAsync(string userName, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(u => u.Email == userName && u.Password == password);

        }
    }
}
