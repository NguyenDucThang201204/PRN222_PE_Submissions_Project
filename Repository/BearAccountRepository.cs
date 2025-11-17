using Microsoft.EntityFrameworkCore;
using Repository.Basic;
using Repository.DBContext;
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
        public BearAccountRepository() { }
        public BearAccountRepository(FA25BearDBContext context) => _context = context;
        public async Task<BearAccount> GetUserAccount(string email, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
        public async Task<List<BearAccount>> GetAllAsync()
        {
            var item = await _context.BearAccounts.ToListAsync();
            return item ?? new List<BearAccount>();
        }
    }
   
    
}
