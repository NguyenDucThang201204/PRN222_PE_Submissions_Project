using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositores.AppDBContext;
using Repositores.Models;
using Repositories.Basic;

namespace Repositores
{
    public class BearProfileRepository : GenericRepository<BearProfile>
    {
        public BearProfileRepository() { }
        private readonly FA25BearDBContext _context;
        public BearProfileRepository(FA25BearDBContext context) => _context = context;
        public async Task<List<BearProfile>> GetAllAsync()
        {
            var items = await _context.BearProfiles
                .Include(c => c.BearType)
                .OrderByDescending(c => c.ModifiedDate)
                .ToListAsync();

            return items ?? new List<BearProfile>();
        }

        public async Task<BearProfile> GetByIdAsync(int id)
        {
            var item = await _context.BearProfiles
                .Include(c => c.BearType)
                .FirstOrDefaultAsync(c => c.BearProfileId == id);

            return item;
        }

        public async Task<List<BearProfile>> SearchAsync(string? BearName, double? weight)
        {
            var items = await _context.BearProfiles
                .Include(c => c.BearType)
                .Where(c =>
                    (string.IsNullOrEmpty(BearName) || c.BearName.Contains(BearName)) &&
                    (weight == null || weight == 0 || c.Weight == weight)
                )
                .ToListAsync();

            return items ?? new List<BearProfile>();
        }
    }
}
