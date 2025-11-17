using BusinessObjects;
using BusinessObjects.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProfileRepo : IProfileRepo
    {
        private readonly Fa25bearDbContext _context;
        public ProfileRepo()
        {
            _context = new Fa25bearDbContext();
        }

        public async Task<GetDTO?> CreateAsync(BearProfile profile)
        {
            _ = _context.BearProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();
            return await GetById(profile.BearProfileId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profile = await _context.BearProfiles.FirstOrDefaultAsync(h => h.BearProfileId == id);
            if (profile == null)
                return false;

            _context.BearProfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return true;
        }
        public Task<BearType?> GetTypeById(int id)
        {
            return _context.BearTypes.FirstOrDefaultAsync(b => b.BearTypeId == id);
        }
        public async Task<List<GetDTO>> GetAllAsync()
        {
            return await _context.BearProfiles
                .Include(h => h.BearType)
                .Select(h => new GetDTO
                {
                    BearProfileId = h.BearProfileId,
                    BearTypeId = h.BearTypeId,
                    BearName = h.BearName,
                    BearWeight = h.BearWeight,
                    Characteristics = h.Characteristics,
                    CareNeeds = h.CareNeeds,
                    ModifiedDate = h.ModifiedDate,
                    BearTypeName = h.BearType != null ? h.BearType.BearTypeName : null,
                    Origin = h.BearType != null ? h.BearType.Origin : null,
                    Description = h.BearType != null ? h.BearType.Description : null,
                })
                .ToListAsync();
        }

        public Task<GetDTO?> GetById(int id)
        {
            return _context.BearProfiles
                .Include(h => h.BearType)
                .Select(h => new GetDTO
                {
                    BearProfileId = h.BearProfileId,
                    BearTypeId = h.BearTypeId,
                    BearName = h.BearName,
                    BearWeight = h.BearWeight,
                    Characteristics = h.Characteristics,
                    CareNeeds = h.CareNeeds,
                    ModifiedDate = h.ModifiedDate,
                    BearTypeName = h.BearType != null ? h.BearType.BearTypeName : null,
                    Origin = h.BearType != null ? h.BearType.Origin : null,
                    Description = h.BearType != null ? h.BearType.Description : null,
                }).FirstOrDefaultAsync(h => h.BearProfileId == id);
        }

        public Task<BearProfile?> GetByIdForUpdate(int id)
        {
            return _context.BearProfiles.FirstOrDefaultAsync(h => h.BearProfileId == id);
        }
        public async Task<GetDTO?> UpdateAsync(BearProfile profile)
        {
            _context.BearProfiles.Update(profile);
            await _context.SaveChangesAsync();
            return await GetById(profile.BearProfileId);
        }
    }
}
