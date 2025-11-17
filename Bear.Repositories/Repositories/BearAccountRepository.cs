using Bear.Repositories.Basic;
using Bear.Repositories.DBContext;
using Bear.Repositories.Interface;
using Bear.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bear.Repositories.Repositories
{
    public class BearAccountRepository : GenericRepository<BearAccount>, IBearAccountRepository
    {
        public BearAccountRepository() { }
        public BearAccountRepository(FA25BearDBContext context) => _context = context;
        public async Task<BearAccount> GetAccountByEmail(string email, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(s => ((s.Email == email) || (s.Username == email)) && s.Password == password);
        }
    }
}
