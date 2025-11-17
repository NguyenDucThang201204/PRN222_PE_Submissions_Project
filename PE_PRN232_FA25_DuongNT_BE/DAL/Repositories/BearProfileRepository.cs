using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BearProfileRepository
    {
        private readonly Fa25bearDbContext _context;

        public BearProfileRepository(Fa25bearDbContext context)
        {
            _context = context;
        }

        public async Task<List<BearProfile>> GetAllAsync()
        {
            return await _context.BearProfiles
                
                .ToListAsync();
        }

        public async Task<BearProfile?> GetByIdAsync(int id)
        {
            return await _context.BearProfiles

                .FirstOrDefaultAsync(h => h.BearProfileId == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.BearProfiles.AnyAsync(h => h.BearProfileId == id);
        }
        public async Task<BearProfile> CreateAsync(BearProfile handbag)
        {
            _context.BearProfiles.Add(handbag);
            await _context.SaveChangesAsync();
            return await _context.BearProfiles

                .FirstAsync(h => h.BearProfileId == handbag.BearProfileId);
        }

        public async Task<BearProfile> UpdateAsync(BearProfile handbag)
        {
            _context.BearProfiles.Update(handbag);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(handbag.BearProfileId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var handbag = await _context.BearProfiles.FindAsync(id);
            if (handbag == null) return false;

            _context.BearProfiles.Remove(handbag);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<BearProfile>> SearchAsync(string? BearName, double? weight)
        {
            var query = _context.BearProfiles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(BearName))
                query = query.Where(h => h.BearName.Contains(BearName));

            if (weight.HasValue)
                query = query.Where(h => h.Weight == weight.Value);

            return await query
                .Include(h => h.BearType) // nếu bạn muốn load thêm loại báo
                .ToListAsync();
        }
    }
}
