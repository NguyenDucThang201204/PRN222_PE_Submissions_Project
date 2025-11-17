using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
	public class BearProfileRepository : GenericRepository<BearProfile>
	{
		public BearProfileRepository()
		{
			
		}

		public BearProfileRepository(FA25BearDBContext context)
		{
			_context = context;
		}

		public async Task<List<BearProfile>> GetAllAsync()
		{
			return await _context.BearProfiles.Include(x => x.BearType).ToListAsync();
		}

		public async Task<BearProfile?> GetByIdAsync(int id)
		{
			return await _context.BearProfiles.Include(x => x.BearType).FirstOrDefaultAsync(x => x.BearProfileId == id);
		}
	}
}
