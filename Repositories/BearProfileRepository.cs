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
    public class BearProfileRepository : GenericRepository<BearProfile>
    {
        public BearProfileRepository() => _context ??= new FA25BearDBContext();
        
        public BearProfileRepository(FA25BearDBContext context) => _context = context;
        
        public async Task<List<BearProfile>> GetAllWithBearTypeAsync()
        {
            return await _context.BearProfiles.Include(h => h.BearType).ToListAsync();
        }
        
        public async Task<BearProfile> GetByIdWithBearTypeAsync(int id)
        {
            return await _context.BearProfiles.Include(h => h.BearType).FirstOrDefaultAsync(h => h.BearProfileId == id);
        }
        
        public async Task<List<BearProfile>> SearchBearProfilesAsync(string BearName)
        {
            var query = _context.BearProfiles.Include(h => h.BearType).AsQueryable();
            
            if (!string.IsNullOrEmpty(BearName))
            {
                query = query.Where(h => h.BearName.Contains(BearName));
            }
            
            return await query.OrderBy(h => h.BearType.BearTypeName).ToListAsync();
        }
    }
}

