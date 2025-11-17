using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository(Fa25bearDbContext _context)
    {
        public async Task<BearAccount?> Login(string email, string password)
        {
            var user = await _context.BearAccounts
                .Where(a => a.Email == email && a.Password == password)
                .FirstOrDefaultAsync();
            return user;
        }
    }
}
