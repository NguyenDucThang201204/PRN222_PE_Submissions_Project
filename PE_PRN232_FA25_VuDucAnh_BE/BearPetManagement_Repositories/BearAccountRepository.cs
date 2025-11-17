using BearPetManagement_Repositories.DBContext;
using BearPetManagement_Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearPetManagement_Repositories
{
    public class BearAccountRepository : GenericRepository<BearAccount>
    {
        public BearAccountRepository() : base() { }
        public BearAccountRepository(FA25BearDBContext context) : base(context) { }
        public async Task<BearAccount> GetAccountAsync(string email, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(sa => sa.Email == email && sa.Password == password);
        }

    }
}
