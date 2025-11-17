using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository
    {
        private readonly Fa25bearDbContext _context;

        public UserRepository(Fa25bearDbContext context)
        {
            _context = context;
        }

        public async Task<BearAccount?> GetByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.BearAccounts
                .FirstOrDefaultAsync(a => a.Email == email && a.Password == password );
        }
    }
}

