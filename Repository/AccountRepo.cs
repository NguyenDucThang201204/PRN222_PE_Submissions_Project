using Microsoft.EntityFrameworkCore;
using Repository.Basic;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepo : GenericRepository<BearAccount>
    {
        public AccountRepo() { }

        public AccountRepo(FA25BearDBContext context) : base(context) { }

        public async Task<BearAccount?> GetUserAccount(string email, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
