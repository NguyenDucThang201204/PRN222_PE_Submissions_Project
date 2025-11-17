using Microsoft.EntityFrameworkCore;
using Repos.Basic;
using Repos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public class SystemAccountsRepo : GenericRepository<BearAccount>
    {
        public SystemAccountsRepo() => _context ??= new
            DBContext.FA2025BearDBContext();
        public SystemAccountsRepo(DBContext.FA2025BearDBContext context) => _context = context;
        public async Task<BearAccount> GetUserAccountAsync(string email, string password)
        {
            return await _context.BearAccount.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);


            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password && u.IsActive == true);
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.Email == userName && u.Password == password && u.IsActive == true);
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.Phone == userName && u.Password == password && u.IsActive == true);
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.EmployeeCode == userName && u.Password == password && u.IsActive == true);
        }
    }
}
