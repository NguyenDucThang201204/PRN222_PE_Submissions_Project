using BearPetManagement_Repository.NewFolder;
using Microsoft.EntityFrameworkCore;

namespace BearPetManagement_Repository.Repositories
{
    public class BearRepository : GenericRepository<BearProfile>
    {
        public override async Task<List<BearProfile>> GetAllAsync()
        {
            return await _context.BearProfiles.Include(b => b.BearType).ToListAsync();
        }
    }
}
