using Microsoft.EntityFrameworkCore;
using PE__PRN232_FA25_NamPK_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE__PRN232_FA25_NamPK_DAL
{
    public class BearAccountRepository : GenericRepository<BearAccount>
    {
        private readonly Fa25bearDbContext _context;

        public BearAccountRepository(Fa25bearDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BearAccount> Authenticate(string email, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(sa => sa.Email == email && sa.Password == password);
        }
    }
}
