using Microsoft.EntityFrameworkCore;
using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class BearAccountRepo:GenericRepository<BearAccount>
    {
        public BearAccountRepo()
        {
        }

        public async Task<BearAccount> GetUserAccount(string username, string password)
        {
            return await _context.BearAccounts
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}
