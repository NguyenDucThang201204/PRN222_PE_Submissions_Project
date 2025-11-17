using BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BearAccountDAO
    {
        private readonly Fa25bearDbContext _context;
        public BearAccountDAO(Fa25bearDbContext context)
        {
            _context = context;
        }
        public BearAccount Login(string userName ,string password) {

            var account = _context.BearAccounts.FirstOrDefault(a => a.Username == userName && a.Password == password);
        return account;    
        }
    }
}
