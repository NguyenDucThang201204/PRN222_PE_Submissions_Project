using Bear.Repositories.Basic;
using Bear.Repositories.DBContext;
using Bear.Repositories.Interface;
using Bear.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bear.Repositories.Repositories
{
    public class BearProfileRepository : GenericRepository<BearProfile>, IBearProfileRepository
    {
        public BearProfileRepository() { }
        public BearProfileRepository(FA25BearDBContext context) => _context = context;
        public async Task<List<BearProfile>> GetAllItemsAsync()
        {
            return await _context.BearProfiles.Include(h => h.BearType).ToListAsync();
        }

        public async Task<BearProfile> GetItemByIdAsync(int id)
        {
            return await _context.BearProfiles.Include(h => h.BearType).FirstOrDefaultAsync(h => h.BearProfileId == id);
        }
    }
}
