using Microsoft.EntityFrameworkCore;
using Repos.Basic;
using Repos.ModelExtensions;
using Repos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public class MainRepo : GenericRepository<BearProfile>
    {
        public MainRepo() => _context ??= new DBContext.FA2025BearDBContext();
        public MainRepo(DBContext.FA2025BearDBContext context) => _context = context;

        public async Task<List<BearProfile>> GetAllAsync()
        {
            var items = await _context.BearProfile.Include(d => d.BearType)
                .ToListAsync();
            return items ?? new List<BearProfile>();
        }

        public async Task<BearProfile?> GetByIdAsync(int id)
        {
            var items = await _context.BearProfile.Include(d => d.BearType)
                .FirstOrDefaultAsync(v => v.BearProfileId == id);
            return items ?? new BearProfile();
        }

        public async Task<List<BearProfile>> SearchAsync(string bearName, decimal? bearWeight, string bearTypeName )
        {
            var items = await _context.BearProfile
                .Include(d => d.BearType)
                .Where(d =>
                    (d.BearName.Contains(bearName) || string.IsNullOrEmpty(bearName))
                    && (bearWeight == null || d.BearWeight == bearWeight)
                    && (d.BearType.BearTypeName.Contains(bearTypeName) || string.IsNullOrEmpty(bearTypeName))
                ).OrderByDescending(d => d.ModifiedDate)
                .ToListAsync();
            return items ?? new List<BearProfile>();
        }

        public async Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(MainSearchRequest searchRequest)
        {
            var items = await this.SearchAsync(searchRequest.BearName, searchRequest.BearWeight.Value,searchRequest.BearTypeName);

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

