using Microsoft.EntityFrameworkCore;
using Repositories.ThienNH.Basic;
using Repositories.ThienNH.DBContext;
using Repositories.ThienNH.ModelExtensions;
using Repositories.ThienNH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ThienNH
{
    public class BearProfileRepository : GenericRepository<BearProfile>
    {
        public BearProfileRepository() { }
        public BearProfileRepository(FA25BearDBContext context) => _context = context;
        public async Task<List<BearProfile>> GetAllAsync()
        {
            var items = await _context.BearProfiles.Include(c => c.BearType).ToListAsync();
            return items ?? new List<BearProfile>();
        }

        public async Task<BearProfile> GetByIdAsync(int id)
        {
            var item = await _context.BearProfiles
               .Include(c => c.BearType)
               .FirstOrDefaultAsync(s => s.BearProfileId == id);
            return item ?? new BearProfile();

        }

        public async Task<List<BearProfile>> SearchAsync(string bearName, double weight, string bearTypeName)
        {
            var bearProfiles = await _context.BearProfiles
                .Include(b => b.BearType)
                .Where(c =>
                    (string.IsNullOrEmpty(bearName) || c.BearName.Contains(bearName)) &&
                    (c.Weight == weight) || string.IsNullOrEmpty(bearTypeName) || c.BearType.BearTypeName.Contains(bearTypeName)
                ).OrderBy(c => c.ModifiedDate).ToListAsync();
            return bearProfiles ?? new List<BearProfile>();
        }

        public async Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(BearProfileSearchRequest searchRequest)
        {
            var items = await this.SearchAsync(searchRequest.BearName, searchRequest.Weight, searchRequest.BearTypeName);
            var totalItems = items.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / searchRequest.pageSize.Value);

            items = items.Skip((searchRequest.currentPage.Value - 1) * searchRequest.pageSize.Value).Take(searchRequest.pageSize.Value).ToList();

            var result = new PaginationResult<List<BearProfile>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = searchRequest.currentPage.Value,
                PageSize = searchRequest.pageSize.Value,
                Items = items
            };

            return result ?? new PaginationResult<List<BearProfile>>();
        }
    }
}
