using BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class BearAccountDAO
    {
        private readonly Fa25bearDbContext _context;
        public BearAccountDAO(Fa25bearDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public BearAccount? GetBearAccount(string email, string password)
        {
            return _context.BearAccounts.SingleOrDefault(m => m.Emal.Equals(email) && m.Password.Equals(password));

        }
    }
}
