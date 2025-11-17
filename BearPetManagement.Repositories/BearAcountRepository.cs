using BearPetManagement.Repositories.Basic;
using BearPetManagement.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearPetManagement.Repositories
{
    public class BearAcountRepository : GenericRepository<BearAccount>
    {
        public BearAcountRepository() { }

        public BearAcountRepository(FA25BearDBContext context)
        => _context = context;

        public async Task<BearAccount?> GetUserAccount(string email, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
