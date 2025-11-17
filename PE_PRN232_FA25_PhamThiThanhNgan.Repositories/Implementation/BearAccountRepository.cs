using Microsoft.EntityFrameworkCore;
using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Interface;
using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Models;


namespace Practice_Fa25_PE_Repositories.Repository
{
    public class BearAccountRepository : IBearAccountRepository
    {
        private readonly Fa25bearDbContext _context;
        public BearAccountRepository(Fa25bearDbContext context)
        {
            _context = context;
        }

        public async Task<BearAccount?> GetAccountByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.BearAccounts
                .FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        }
    }
}