using Microsoft.EntityFrameworkCore;
using PE_PRN232_FA25_PhanTuanAn_BE.Repositories.DBContext;
using PE_PRN232_FA25_PhanTuanAn_BE.Repositories.Models;
using Repositories.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_PhanTuanAn_BE.Repositories
{
    public class BearAccountRepository : GenericRepository<BearAccount>
    {
        public BearAccountRepository() { }
        public BearAccountRepository(FA25BearDBContext context) => _context = context;

        public async Task<BearAccount> GetUserAccount(string email, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
