using Microsoft.EntityFrameworkCore;
using PE_PRN232_FA25_DaoTrongTien_BE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_DaoTrongTien_BE.DAL.Repositories
{
    public class BearAccountRepository:IBearAccountRepository
    {

        private Fa25bearDbContext _context;
        public BearAccountRepository(Fa25bearDbContext context)
        {
            _context = context;
        }
        public async Task<BearAccount> GetBearAccount(string email, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

        }

    }
}
