using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.DBContext;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BearAccountRepository : GenericRepository<BearAccount>
    {
        public BearAccountRepository() => _context ??= new FA25BearDBContext();
        
        public BearAccountRepository(FA25BearDBContext context) => _context = context;
        
        public async Task<BearAccount> GetUserAccountAsync(string userName, string password)
        {

            return await _context.BearAccounts.FirstOrDefaultAsync(u => u.Email == userName && u.Password == password);


            //return await _context.SystemAccounts.FirstOrDefaultAsync(u => u.Username == userName && u.Password == password && u.IsActive == true);

            //return await _context.SystemAccounts.FirstOrDefaultAsync(u => u.Email == userName && u.Password == password && u.IsActive == true);

            //return await _context.SystemAccounts.FirstOrDefaultAsync(u => u.Phone == userName && u.Password == password && u.IsActive == true);

            //return await _context.SystemAccounts.FirstOrDefaultAsync(u => u.EmployeeCode == userName && u.Password == password && u.IsActive == true);
        }
    }
}
