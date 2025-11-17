using Microsoft.EntityFrameworkCore;
using Repositories.Base;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class BearProfileRepo : GenericRepository<BearProfile>
    {
        public async Task<List<BearProfile>> GetBearProfilesAsync()
        {
            return await _context.BearProfiles
                .Include(h => h.BearType)
                .OrderByDescending(h => h.BearProfileId)
                .ToListAsync();
        }

        public async Task<BearProfile?> GetBearProfileByIdAsync(int id)
        {
            return await _context.BearProfiles
                .Include(h => h.BearType)
                .FirstOrDefaultAsync(h => h.BearProfileId == id);
        }

        public async Task<int?> CreateAsync(BearProfile bear)
        {
            return await CreateAsync(bear);
        }

        public async Task<int?> UpdateAsync(BearProfile bear)
        {
            return await UpdateAsync(bear);
        }

        public async Task<bool> DeleteAsync(BearProfile bear)
        {
            return await RemoveAsync(bear);
        }
    }
}
