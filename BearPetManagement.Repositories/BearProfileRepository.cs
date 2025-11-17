using BearPetManagement.Repositories.Basic;
using BearPetManagement.Repositories.ModelExtensions;
using BearPetManagement.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearPetManagement.Repositories
{
    public class BearProfileRepository : GenericRepository<BearProfile>
    {
        public BearProfileRepository() => _context ??= new FA25BearDBContext();

        public BearProfileRepository(FA25BearDBContext context) => _context = context;
        public async Task<List<BearProfile>> GetAllAsync()
        {
            var bears = await _context.BearProfiles
                                    .Include(t => t.BearType).ToListAsync();
            return bears;
        }

        public async Task<BearProfile> GetById(int code)
        {
            var bears = await _context.BearProfiles.Include(t => t.BearType).FirstOrDefaultAsync(r => r.BearProfileId == code);
            return bears;
        }
        public async Task<List<BearProfile>> SearchAsync(string bearName, decimal? bearWeight, string bearTypeName)
        {

            var items = await _context.BearProfiles
                .Include(r => r.BearType)
                .Where(re =>
                (re.BearName.Contains(bearName) || string.IsNullOrEmpty(bearName)) &&
                (re.BearWeight == bearWeight || bearWeight == 0 || bearWeight == null) &&
                (re.BearType.BearTypeName.Contains(bearTypeName) || string.IsNullOrEmpty(bearTypeName))
                ).ToListAsync();

            return items;
        }
        public async Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(int currentPage, int pageSize, string bearName, decimal? bearWeight, string bearTypeName)
        {
            var items = await SearchAsync(bearName, bearWeight, bearTypeName);


            var totalItems = items.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            items = items.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var results = new PaginationResult<List<BearProfile>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = items
            };
            return results;
        }
    }
}
