using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.ModelExtensions;
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
        public BearProfileRepository()
        {
        }

        public BearProfileRepository(FA25BearDBContext context)
        {
            context = _context;
        }

        public async Task<List<BearProfile>> GetAllASync()
        {
            var items = await _context.BearProfiles
                .Include(h => h.BearType)
                .ToListAsync();

            return items ?? new List<BearProfile>();
        }

        public async Task<BearProfile> GetByIdAsync(int id)
        {
            var item = await _context.BearProfiles
                .Include(h => h.BearType)
                .FirstOrDefaultAsync(h => h.BearProfileId == id);
            return item;
        }

        public async Task<List<BearProfile>> SearchAsync(string? BearName, double? BearWeight)
        {
            var items = await _context.BearProfiles
                .Include(h => h.BearType)
                .Where(h => (BearName == null || h.BearName.Contains(BearName)) &&
                            (BearWeight == null || h.BearWeight == BearWeight))
                .OrderByDescending(h => h.BearProfileId)
                .ToListAsync();

            return items;
        }

        public async Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(BearProfileRequest searchRequest)
        {
            var items = await this.SearchAsync(
                searchRequest.BearName,
                searchRequest.BearWeight
            );

            var totalItems = items.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / searchRequest.PageSize.Value);

            var currentPage = searchRequest.CurrentPage ?? 1;
            var pageSize = searchRequest.PageSize ?? 3;

            var pagedItems = items
               .Skip((currentPage - 1) * pageSize)
               .Take(pageSize)
               .ToList();

            var result = new PaginationResult<List<BearProfile>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = pagedItems
            };
            return result;
        }
    }
}
