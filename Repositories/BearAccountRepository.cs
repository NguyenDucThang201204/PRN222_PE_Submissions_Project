using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BearAccountRepository
    {
        protected readonly FA25BearDBContext _context;

        public BearAccountRepository()
        {
            _context ??= new();
        }

        public BearAccountRepository(FA25BearDBContext context)
        {
            _context = context;
        }

        public async Task<BearAccount?> GetUserAccount(string email, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            //return await _context.SystemAccounts.FirstOrDefaultAsync(u => u.Email == email && u.Password == password && u.IsActive == true);
        }
    }
}
