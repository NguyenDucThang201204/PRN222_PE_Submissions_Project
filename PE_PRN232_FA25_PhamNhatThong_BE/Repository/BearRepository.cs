using Microsoft.EntityFrameworkCore;
using Repository.MExtension;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BearRepository(Fa25bearDbContext _context)
    {
        public IQueryable<BearProfile> GetDatas()
        {
            return _context.BearProfiles.AsQueryable();
        }

        public async Task<IEnumerable<BearProfile>> GetAll()
        {
            return await _context.BearProfiles
                .OrderByDescending(i => i.BearProfileId)
                .Include(i => i.BearType)
                .ToListAsync();
        }

        public async Task<BearProfile?> GetById(int id)
        {
            var item = await _context.BearProfiles.Include(i => i.BearType).FirstOrDefaultAsync(i => i.BearProfileId == id);

            return item;
        }

        public async Task<BearProfile> Create(BearProfile request)
        {
            _context.BearProfiles.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<BearProfile?> Update(BearProfile request)
        {
            var item = await _context.BearProfiles.FindAsync(request.BearProfileId);
            if (item == null)
            {
                return null;
            }
            _context.Entry(item).CurrentValues.SetValues(request);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _context.BearProfiles.FindAsync(id);
            if (item == null)
            {
                return false;
            }
            _context.BearProfiles.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }


        // No group
        public async Task<IEnumerable<BearProfile>> Search(string? search1, double? search2, string? search3)
        {
            var query = _context.BearProfiles
                .Include(i => i.BearType)
                .Where(h =>
                    (string.IsNullOrWhiteSpace(search1) || h.BearName.ToLower().Contains(search1.ToLower()))
                    && (!search2.HasValue || h.BearWeight == search2.Value)
                    && (string.IsNullOrWhiteSpace(search3) || h.BearType.BearTypeName.ToLower().Contains(search3.ToLower()))
                );

            return await query.ToListAsync();
        }

        // group
        public async Task<IEnumerable<BearProfile>> SearchGroup(string? search1, double? search2, string? search3)
        {
            var query = _context.BearProfiles
                .Include(i => i.BearType)
                .Where(i =>
                    (string.IsNullOrWhiteSpace(search1) || i.BearName.ToLower().Contains(search1.ToLower()))
                    && (!search2.HasValue || i. BearWeight == search2.Value)
                    && (string.IsNullOrWhiteSpace(search3) || i.BearType.BearTypeName.ToLower().Contains(search3.ToLower()))
                );
            var result = await query.ToListAsync();

            return [.. result.GroupBy(h => h.BearType.BearTypeName).SelectMany(h => h.OrderBy(h => h.BearWeight))];
        }


        public async Task<PaginationResult<BearProfile>> GetAllPaging(int page, int pageSize)
        {
            var query = _context.BearProfiles.Include(i => i.BearType).AsQueryable();
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationResult<BearProfile>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = items
            };
        }

        // phu
        public async Task<BearType?> GetTypeById(int id)
        {
            var item = await _context.BearTypes.FirstOrDefaultAsync(i => i.BearTypeId == id);
            return item;
        }
    }
}
