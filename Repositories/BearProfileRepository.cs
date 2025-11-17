using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using Repository.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BearProfileRepository
    {
        protected readonly FA25BearDBContext _context;
        public BearProfileRepository()
        {
            _context ??= new();
        }

        public BearProfileRepository(FA25BearDBContext context) => _context = context;

        public async Task<List<BearProfile>> GetAllAsync() => await _context.BearProfiles
            .Include(i => i.BearType)
            .OrderByDescending(i => i.BearProfileId)
            .ToListAsync();

        public IQueryable<BearProfile> GetQueryable()
        {
            return _context.BearProfiles
                .Include(i => i.BearType)
                .AsQueryable();
        }

        public async Task<BearProfile?> GetByIdAsync(int id) => await _context.BearProfiles.Where(i => i.BearProfileId == id).Include(i => i.BearType).FirstOrDefaultAsync();

        public async Task<int> CreateAsync(BearProfile entity)
        {
            entity.BearProfileId = _context.BearProfiles.Max(x => x.BearProfileId) + 1;

            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(BearProfile entity)
        {
            _context.ChangeTracker.Clear();
            entity.BearType = null;
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveAsync(BearProfile entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BearProfile>> SearchAsync(string? bearName, string? bearTypeName, decimal? bearWeight)
        {
            return await _context.BearProfiles.Include(i => i.BearType)
                .Where(i => (i.BearName.Contains(bearName) || string.IsNullOrEmpty(bearName))
                        && (i.BearType.BearTypeName.Contains(bearTypeName) || string.IsNullOrEmpty(bearTypeName))
                        && (i.BearWeight == bearWeight || bearWeight == 0 || bearWeight == null))
                .OrderByDescending(i => i.BearProfileId)
                .ToListAsync() ?? new List<BearProfile>();
        }

        public async Task<PaginationResult<List<BearProfile>>> SearchWithPaginationAsync(MainSearchRequest request)
        {
            var items = await this.SearchAsync(request.BearName, request.BearTypeName, request.BearWeight);
            var totalItems = items.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize.Value);

            items = items.Skip((request.CurrentPage.Value - 1) * request.PageSize.Value).Take(request.PageSize.Value).ToList();

            var result = new PaginationResult<List<BearProfile>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPages = request.CurrentPage.Value,
                PageSizes = request.PageSize.Value,
                Items = items
            };
            return result;
        }
    }
}
