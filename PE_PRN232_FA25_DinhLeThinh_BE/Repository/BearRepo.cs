using Microsoft.EntityFrameworkCore;
using Model;
using Model.DTOs;

namespace Repository
{
    public class BearRepo : IBearRepo
    {
        private readonly Fa25bearDbContext _context;
        public BearRepo()
        {
            _context = new Fa25bearDbContext();
        }

        public async Task<BearDto?> CreateAsync(BearProfile bear)
        {
            _ = _context.BearProfiles.Add(bear);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(bear.BearProfileId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var bear = await _context.BearProfiles.FirstOrDefaultAsync(b => b.BearProfileId == id);
            if (bear == null)
                return false;

            _context.BearProfiles.Remove(bear);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BearDto>> GetAllAsync()
        {
            return await _context.BearProfiles
                .Include(b => b.BearType)
                .Select(b => new BearDto
                {
                    BearProfileId = b.BearProfileId,
                    BearTypeId = b.BearTypeId,
                    BearTypeName = b.BearType != null ? b.BearType.BearTypeName : string.Empty,
                    BearName = b.BearName,
                    BearWeight = b.BearWeight,
                    Characteristics = b.Characteristics,
                    CareNeeds = b.CareNeeds,
                    ModifiedDate = b.ModifiedDate
                })
                .ToListAsync();
        }

        public async Task<BearDto?> GetByIdAsync(int id)
        {
            return await _context.BearProfiles
                .Include(b => b.BearType)
                .Select(b => new BearDto
                {
                    BearProfileId = b.BearProfileId,
                    BearTypeId = b.BearTypeId,
                    BearTypeName = b.BearType != null ? b.BearType.BearTypeName : string.Empty,
                    BearName = b.BearName,
                    BearWeight = b.BearWeight,
                    Characteristics = b.Characteristics,
                    CareNeeds = b.CareNeeds,
                    ModifiedDate = b.ModifiedDate
                })
                .FirstOrDefaultAsync(b => b.BearProfileId == id);
        }

        public async Task<BearProfile?> GetByIdAsyncNoDto(int id)
        {
            return await _context.BearProfiles.FirstOrDefaultAsync(b => b.BearProfileId == id);
        }

        public Task<BearDto?> UpdateAsync(BearProfile id)
        {
            throw new NotImplementedException();
        }
    }
}
