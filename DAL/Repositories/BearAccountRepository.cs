using DAL.Entities;
using DAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BearAccountRepository  : GenericRepository<BearAccount>, IBearAccountRepository
    {
        public BearAccountRepository(Fa25bearDbContext context) : base(context)
        {
        }
        public async Task<BearAccount> LoginAsync(string email, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(e => (e.Email == email) && e.Password == password);
        }
    }
}
