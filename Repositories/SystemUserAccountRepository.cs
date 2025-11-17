using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.Models;

namespace Repositories

{
    public class SystemUserAccountRepository : GenericRepository<BearAccount>
    {
        public SystemUserAccountRepository() => _context ??= new FA25BearDBContext();

        public SystemUserAccountRepository(FA25BearDBContext context) => _context = context;

        public async Task<BearAccount> GetUserAccountAsync(string userName, string password)
        {
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password && u.IsActive == true);

            return await _context.BearAccounts.FirstOrDefaultAsync(u => u.Email == userName && u.Password == password);
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.Phone == userName && u.Password == password && u.IsActive == true);
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.EmployeeCode == userName && u.Password == password && u.IsActive == true);
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
        }
    }
}
