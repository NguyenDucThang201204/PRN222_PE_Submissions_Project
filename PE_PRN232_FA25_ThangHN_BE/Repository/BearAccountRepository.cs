using Microsoft.EntityFrameworkCore;
using Repository.Basic;
using Repository.Dbcontext;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BearAccountRepository : GenericRepository<BearAccount>
    {
        public BearAccountRepository(FA25BearDBContext context) : base(context) { }
        public async Task<BearAccount?> GetAccountAsync(string email, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        }
    }
}
