using BearPetManagement_Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace BearPetManagement_Repository.Repositories
{
    public class UserRepository : GenericRepository<BearAccount>
    {

        public async Task<BearAccount> GetUserByEmailPassword(string username, string password)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }

        public async Task<List<int>> GetAllRoleId()
        {
            return await _context.BearAccounts.Select(u => u.RoleId).ToListAsync();
        }

    }
}
