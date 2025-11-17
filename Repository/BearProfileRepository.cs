using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Repository.ModelExtensions;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BearProfileRepository : Basic.GenericRepository<BearProfile>
    {
        public BearProfileRepository() { }
        public BearProfileRepository(FA25BearDBContext context) => _context = context;
        public new async Task<List<BearProfile>> GetAllAsync()
        {
            var item = await _context.BearProfiles.Include(x => x.BearType).OrderByDescending(x => x.ModifiedDate).ToListAsync();
            return item ;
        }
        public new async Task<BearProfile> GetByIdAsync(int id)
        {
            var item = await _context.BearProfiles.Include(x => x.BearType).FirstOrDefaultAsync(x => x.BearProfileId == id);
            return item ;
        }

        public async Task<List<BearProfile>> SearchAsync(string? typename, double? weight,string? name)
        {
            var items = await _context.BearProfiles
                .Include(x => x.BearType)
                .Where(x => x.BearType.BearTypeName.Contains(typename) || (string.IsNullOrEmpty(typename)) &&
                            (weight == 0 || x.BearWeight == weight) && (x.BearName.Contains(name) || (string.IsNullOrEmpty(name))))
                .OrderByDescending(x => x.ModifiedDate)
                .ToListAsync();
            return items ;
        }
        public async Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(BearProfileSearchRequest request)
        {
            var items = await this.SearchAsync(request.typename, request.weight, request.name);
            var totalItems = items.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / request.pageSize.Value);

            items = items.Skip((request.currentPage.Value - 1) * request.pageSize.Value).Take(request.pageSize.Value).ToList();

            var result = new PaginationResult<List<BearProfile>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = request.currentPage.Value,
                PageSize = request.pageSize.Value,
                Items = items
            };
            return result ?? new PaginationResult<List<BearProfile>>();
        }
    }
}
