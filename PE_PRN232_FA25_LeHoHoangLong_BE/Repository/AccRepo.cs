using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccRepo : IAccRepo
    {
        private readonly Fa25bearDbContext _context;
        public AccRepo(Fa25bearDbContext context)
        {
            _context = context;
        }

        public async Task<BearAccount?> LoginAsync(string email, string password)
        {
            return await _context.BearAccounts
                .FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        }
    }
}
