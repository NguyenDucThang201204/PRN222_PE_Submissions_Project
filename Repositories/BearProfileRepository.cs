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
        private readonly FA25BearDBContext _context;

        public BearProfileRepository() => _context ??= new FA25BearDBContext();

        public async Task<List<BearProfile>> GetAllAsync()
        {
            return await _context.BearProfiles.Include(h => h.BearType).ToListAsync();
        }

        public async Task<BearProfile?> GetByIdAsync(int id)
        {
            return await _context.BearProfiles.Include(c => c.BearType).FirstOrDefaultAsync(x => x.BearProfileId == id);
        }

      

        public async Task<List<BearProfile>> SearchAsync(string bearName, int bearWeight, string bearTypeName)
        {
            var a = await _context.BearProfiles.Include(h => h.BearType)
                .Where(x =>
                    (x.BearName.Contains(bearName) || string.IsNullOrEmpty(bearName))
                    && (x.BearType.BearTypeName.Contains(bearTypeName) || string.IsNullOrEmpty(bearTypeName))
                    && (x.BearWeight == bearWeight || bearWeight == 0)
                    ).ToListAsync();
            return a ?? new List<BearProfile>();
        }

        public async Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(string bearName, int bearWeight, string bearTypeName, int currentPage, int pageSize)
        {
            var term = await this.SearchAsync(bearName, bearWeight, bearTypeName);

            var totalCount = term.Count;
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            term = term
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PaginationResult<List<BearProfile>>
            {
                TotalItems = totalCount,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = term
            };

            return result;
        }
    }
}
