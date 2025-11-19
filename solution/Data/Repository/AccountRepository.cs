using Data.IRepository;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly Su25pantherDbContext _context;

        public AccountRepository(Su25pantherDbContext context)
        {
            _context = context;
        }

        public PantherAccount GetByEmail(string email)
        {
            return _context.PantherAccounts.FirstOrDefault(a => a.Email == email);
        }
    }
}
