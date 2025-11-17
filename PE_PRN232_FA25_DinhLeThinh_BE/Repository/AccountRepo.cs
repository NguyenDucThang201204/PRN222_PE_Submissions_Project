using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly Fa25bearDbContext _context;
        public AccountRepo()
        {
            _context = new Fa25bearDbContext();
        }

        public async Task<BearAccount?> LoginAsync(string email, string pass)
        {
            return await _context.BearAccounts.FirstOrDefaultAsync(a => a.Email == email && a.Password == pass);
        }
    }
}
