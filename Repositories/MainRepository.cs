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
    public class MainRepository : GenericRepository<BearProfile>
    {
        public MainRepository() => _context ??= new SU25BearDBContext();

        public MainRepository(SU25BearDBContext context) => _context = context;

        public async Task<List<BearProfile>> GetAllAsync()
        {
            var items = await _context.BearProfiles.Include(d => d.BearType).ToListAsync();

            return items ?? new List<BearProfile>();
        }

        public async Task<BearProfile> GetByIdAsync(int id)
        {
            var item = await _context.BearProfiles.Include(d => d.BearType).FirstOrDefaultAsync(d => d.BearProfileId == id);
            return item ?? new BearProfile();
        }
    }
}
