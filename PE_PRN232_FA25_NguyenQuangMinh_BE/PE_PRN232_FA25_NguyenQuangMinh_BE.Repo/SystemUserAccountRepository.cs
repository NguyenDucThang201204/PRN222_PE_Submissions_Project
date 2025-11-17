using Microsoft.EntityFrameworkCore;
using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.Basic;
using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenQuangMinh_BE.Repo
{
    public class SystemUserAccountRepository : GenericRepository<BearAccount>
    {
        public SystemUserAccountRepository() => _context ??= new
            DBContext.FA25BearDBContext();
        public SystemUserAccountRepository(DBContext.FA25BearDBContext context) => _context = context;
        public async Task<BearAccount> GetUserAccountAsync(string userName, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(u => u.Email == userName && u.Password == password);


            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password && u.IsActive == true);
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.Email == userName && u.Password == password && u.IsActive == true);
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.Phone == userName && u.Password == password && u.IsActive == true);
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.EmployeeCode == userName && u.Password == password && u.IsActive == true);
        }
    }
}
