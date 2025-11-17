using Microsoft.EntityFrameworkCore;
using PE_PRN232_FA25_NguyenThaiDuy_BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenThaiDuy_BE.Repositories
{
    public class BearProfileRepository : IBearProfileRepository
    {
    private readonly FA25BearDBContext _context;

    public BearProfileRepository(FA25BearDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BearProfile>> GetAllAsync()
    {
        return await _context.BearProfiles
            .Include(br => br.BearType)
            .ToListAsync();
    }

    public async Task<BearProfile?> GetByIdAsync(int id)
    {
        return await _context.BearProfiles
            .Include(br => br.BearType)
            .FirstOrDefaultAsync(br => br.BearProfileId == id);
    }

    public async Task<BearProfile> CreateAsync(BearProfileDto dto)
    {
        var entity = new BearProfile
        {
            BearTypeId = dto.BearTypeId,
            BearName = dto.BearName,
            BearWeight = dto.BearWeight,
            Characteristics = dto.Characteristics,
            CareNeeds = dto.CareNeeds,
            ModifiedDate = DateTime.Now
        };

        _context.BearProfiles.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(int id, BearProfileDto dto)
    {
        var existing = await _context.BearProfiles.FindAsync(id);
        if (existing == null)
            return false;

        existing.BearTypeId = dto.BearTypeId;
        existing.BearName = dto.BearName;
        existing.BearWeight = dto.BearWeight;
        existing.Characteristics = dto.Characteristics;
        existing.CareNeeds = dto.CareNeeds;
        existing.ModifiedDate = DateTime.Now;

        _context.BearProfiles.Update(existing);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.BearProfiles.FindAsync(id);
        if (existing == null)
            return false;

        _context.BearProfiles.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<BearProfile>> SearchAsync(string? bearName, double? bearWeight)
    {
        var query = _context.BearProfiles.AsQueryable();

        if (!string.IsNullOrWhiteSpace(bearName))
        {
            query = query.Where(br => br.BearName.Contains(bearName));
        }

        if (bearWeight.HasValue)
        {
            query = query.Where(br => br.BearWeight == bearWeight.Value);
        }

        return await query
            .Include(br => br.BearType)
            .ToListAsync();
    }
}
}
