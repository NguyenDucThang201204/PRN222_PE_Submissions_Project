using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class UserRepo
    {
        private readonly Fa25bearDbContext _context;

        public UserRepo(Fa25bearDbContext context)
        {
            _context = context;
        }

        public BearAccount? Login(string email)
        {
            return _context.BearAccounts.FirstOrDefault(user => user.Email == email);
        }
    }
}
