using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repository.Basic;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Implements
{
    public class BearAccountRepository: GenericRepository<BearAccount>, IBearAccountRepository
    {
        public BearAccountRepository(FA25BearDBContext context) : base(context)
        {
        }

        public async Task<BearAccount> GetByEmailAsync(string email)
        {
            return await _context.BearAccounts
                .FirstOrDefaultAsync(a => a.Email == email );
        }
    }
}
