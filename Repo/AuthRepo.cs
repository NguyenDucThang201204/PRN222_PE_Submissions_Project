using Microsoft.EntityFrameworkCore;
using pregTrackSys.Repositories.Base;
using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public interface IAuthRepo
    {
        Task<BearAccount> GetAccount(string email, string password);
    }

    public class AuthRepo : GenericRepository<BearAccount>, IAuthRepo
    {
        public AuthRepo() { }

        public async Task<BearAccount> GetAccount(string email, string password)
        {
            var account = await _context
                .BearAccount
                .FirstOrDefaultAsync(acc => acc.Email == email && acc.Password == password);
            return account;
        }
    }
}
