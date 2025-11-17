using PE_PRN232_FA25_AnhTHQ_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_AnhTHQ_DAL.Repostitories
{
    public class BearAccountRepos
    {
        private readonly FA25BearDBContext _context;

        public BearAccountRepos()
        {
            _context = new FA25BearDBContext();

        }

        public BearAccount Login(string email, string password)
        {
            return _context.BearAccounts
                .FirstOrDefault(a => a.Email == email && a.Password == password);
        }
    }
}
