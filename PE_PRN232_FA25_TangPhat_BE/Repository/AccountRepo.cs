using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly Fa25bearDbContext _context;
        public AccountRepo()
        {
            _context = new Fa25bearDbContext();
        }

        public async Task<BearAccount?> LoginAsync(string email, string pass)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(a => a.Email == email && a.Password == pass);
        }
    }
}
