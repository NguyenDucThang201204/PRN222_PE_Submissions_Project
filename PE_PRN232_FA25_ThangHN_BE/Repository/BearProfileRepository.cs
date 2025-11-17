using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Basic;
using Repository.Dbcontext;
using Repository.ModelExtensions;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BearProfileRepository : GenericRepository<BearProfile>
    {
        public BearProfileRepository(FA25BearDBContext context) : base(context) { }
        public async Task<List<BearProfile>> getAllAsync()
        {
            var query = GetQueryable();
            query = query.OrderByDescending(b => b.BearProfileId);
            return await query.ToListAsync();
        }
        public async Task<BearProfile> getByIdAsync(int id) => await GetQueryable().FirstOrDefaultAsync(b => b.BearProfileId == id);
        public async Task<int> CreateAsync(BearProfile entity)
        {
            return await base.CreateAsync(entity);
        }

        public async Task<int> UpdateAsync(BearProfile entity)
        {
            return await base.UpdateAsync(entity);
        }
        public async Task<List<BearProfile>> SearchAsync(string? bearName, double? bearWeight, string? bearTypeName)
        {
            var query = GetQueryable();

            if (!string.IsNullOrWhiteSpace(bearName))
            {
                query = query.Where(h => h.BearName.Contains(bearName));
            }

            if (!bearName.IsNullOrEmpty())
            {
                query = query.Where(h => h.BearWeight == bearWeight);
            }

            if (!string.IsNullOrWhiteSpace(bearTypeName))
            {
                query = query.Where(h => h.BearType.BearTypeName.Contains(bearTypeName));
            }

            query = query.OrderByDescending(h => h.BearProfileId);

            return await query.ToListAsync();
        }
        public async Task<PaginationResult<List<BearProfile>>> SearchWithPaginationAsync(SearchRequestDto request)
        {
            var items = await SearchAsync(request.BearName, request.BearWeight, request.BearTypeName);
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
        public IQueryable<BearProfile> GetQueryable() => _context.BearProfiles.Include(t => t.BearType).AsQueryable();
    }
}
