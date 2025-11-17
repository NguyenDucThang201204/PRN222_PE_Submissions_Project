using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
	public class BearAccountRepository : GenericRepository<BearAccount>
	{
		public BearAccountRepository()
		{
			
		}

		public BearAccountRepository(FA25BearDBContext context)
		{
			_context = context;
		}

		public async Task<BearAccount?> GetUserAccountAsync(string userName, string password)
		{
			var systemUserAccounts = await _context.BearAccounts.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);
			return systemUserAccounts;
		}
	}
}
