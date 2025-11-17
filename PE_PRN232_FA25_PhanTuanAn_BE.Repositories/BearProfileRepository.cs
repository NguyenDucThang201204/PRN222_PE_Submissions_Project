using Microsoft.EntityFrameworkCore;
using PE_PRN232_FA25_PhanTuanAn_BE.Repositories.DBContext;
using PE_PRN232_FA25_PhanTuanAn_BE.Repositories.Models;
using Repositories.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_PhanTuanAn_BE.Repositories
{
    public class BearProfileRepository : GenericRepository<BearProfile>
    {
        public BearProfileRepository() { }

        public BearProfileRepository(FA25BearDBContext context) => _context = context;

        public async Task<List<BearProfile>> GetAllAsync()
        {
            var items = await _context.BearProfiles.Include(c => c.BearType).ToListAsync();

            return items ?? new List<BearProfile>();
        }

        public async Task<BearProfile> GetByIdAsync(int id)
        {
            var item = await _context.BearProfiles
                .Include(s => s.BearType)
                .FirstOrDefaultAsync(s => s.BearTypeId == id);
            return item ?? new BearProfile();
        }
    }
}
