using Microsoft.EntityFrameworkCore;
using PE_PRN232_FA25_AnhTHQ_DAL.Basics;
using PE_PRN232_FA25_AnhTHQ_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_AnhTHQ_DAL.Repostitories
{
    public class BearProfileRepos : GenericRepository<BearProfile>
    {
        private readonly FA25BearDBContext _context;

        public BearProfileRepos(FA25BearDBContext context)
        {
            _context = context;
        }

        public async Task<List<BearProfile>> GetAllAsync()
        {
            var handBags = await _context.BearProfiles
                .Include(h => h.BearType)
                .OrderByDescending(h => h.BearProfileId)
                .ToListAsync();

            return handBags ?? new List<BearProfile>();
        }

        public async Task<BearProfile> GetByIdAsync(int id)
        {
            return await _context.BearProfiles
                .Include(h => h.BearType)
                .FirstOrDefaultAsync(h => h.BearProfileId == id);
        }


        // Search bear profiles by name, weight, and type using relative search
        public async Task<IEnumerable<BearProfile>> SearchAsync(string bearName, int bearWeight, string bearTypeName)
        {
            var query = _context.BearProfiles
                .Include(h => h.BearType)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(bearName))
            {
                query = query.Where(h => h.BearName.Contains(bearName));
            }
            if (bearWeight > 0)
            {
                query = query.Where(h => h.BearWeight == bearWeight);
            }
            if (!string.IsNullOrWhiteSpace(bearTypeName))
            {
                query = query.Where(h => h.BearType.BearTypeName.Contains(bearTypeName));
            }
            return await query.ToListAsync();
        }
            
        
    }
}
