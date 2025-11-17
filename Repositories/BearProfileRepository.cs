using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories.Basic;
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

        public BearProfileRepository() { }
        public BearProfileRepository(FA25BearDBContext context) => _context = context;
        public async Task<List<BearProfile>> GetAllAsync()
        {
            var items = await _context.BearProfiles
                .Include(c => c.BearType)
                .OrderByDescending(c => c.BearProfileId)
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

        public async Task<List<BearProfile>> SearchAsync(string? bearName, double? bearWeight, string? bearTypeName)
        {
            var items = await _context.BearProfiles
                .Include(c => c.BearType)
                .Where(c =>
                    (string.IsNullOrEmpty(bearName) || c.BearName.Contains(bearName)) &&
                    (bearWeight == null || bearWeight == 0 || c.BearWeight == bearWeight) &&
                    (string.IsNullOrEmpty(bearTypeName) || c.BearType.BearTypeName.Contains(bearTypeName))
                )
                .ToListAsync();

            return items ?? new List<BearProfile>();
        }
    }
}
