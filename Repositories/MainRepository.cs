using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.ModelExtensions;
using Repositories.Models;

namespace Repositories
{
    public class MainRepository : GenericRepository<BearProfile>
    {
        public MainRepository() => _context ??= new FA25BearDBContext();

        public MainRepository(FA25BearDBContext context) => _context = context;

        public async Task<List<BearProfile>> GetAllAsync()
        {
            var items = await _context.BearProfiles.Include(d => d.BearType).ToListAsync();

            return items ?? new List<BearProfile>();
        }

        public async Task<BearProfile> GetByIdAsync(int id)
        {
            var item = await _context.BearProfiles.Include(d => d.BearType).FirstOrDefaultAsync(d => d.BearProfileId == id);
            return item ?? new BearProfile();
        }

        public async Task<List<BearProfile>> SearchAsync(string? BearName, double? BearWeight, string? BearTypeName)
        {
            var items = await _context.BearProfiles
                .Include(d => d.BearType)
                .Where(d => (d.BearName.Contains(BearName) || string.IsNullOrEmpty(BearName))
                && (d.BearWeight == BearWeight || BearWeight == 0 || BearWeight == null)
                && (d.BearType.BearTypeName.Contains(BearTypeName) || string.IsNullOrEmpty(BearTypeName))
                ).OrderByDescending(d => d.ModifiedDate)
                .ToListAsync();
            return items ?? new List<BearProfile>();
        }

        public async Task<PaginationResult<List<BearProfile>>> SearchPagingAsync(MainSearchRequest searchRequest)
        {
            var items = await this.SearchAsync(searchRequest.BearName, searchRequest.BearWeight, searchRequest.BearTypeName);
            var totalItems = items.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / searchRequest.PageSize.Value);

            items = items.Skip((searchRequest.CurrentPage.Value - 1) * searchRequest.PageSize.Value).Take(searchRequest.PageSize.Value).ToList();

            var result = new PaginationResult<List<BearProfile>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = searchRequest.CurrentPage.Value,
                PageSize = searchRequest.PageSize.Value,
                Items = items
            };

            return result ?? new PaginationResult<List<BearProfile>>();
        }
    }
}
