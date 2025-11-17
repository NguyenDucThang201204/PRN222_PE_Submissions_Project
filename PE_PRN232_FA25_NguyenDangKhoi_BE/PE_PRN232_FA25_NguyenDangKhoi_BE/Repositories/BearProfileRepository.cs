using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.Models;
using Repositories.ModelExtensions;

namespace Repositories
{
    public class BearProfileRepository : GenericRepository<BearProfile>
    {
        public BearProfileRepository(BearManagementDbContext context) : base(context)
        {
        }

        public async Task<List<BearProfile>> GetAllAsync()
        {
            var query = GetQueryable();

            query = query.OrderByDescending(i => i.BearProfileId).AsNoTracking();
            return await query.ToListAsync();
        }


        public IQueryable<BearProfile> GetQueryable()
            => _dbSet
            .Include(i => i.BearType)
            .AsQueryable();


        public override async Task<BearProfile?> GetByIdAsync(int id) 
            => await GetQueryable().FirstAsync(h => h.BearProfileId == id);

        public async Task<int> CreateAsync(BearProfile entity)
        {
             entity.BearProfileId = _dbSet.Max(x => x.BearProfileId) + 1;

            return await base.CreateAsync(entity);
        }

        public async Task<int> UpdateAsync(BearProfile entity)
        {
            entity.BearType = null;
            return await base.UpdateAsync(entity);
        }

        public async Task<List<BearProfile>> SearchAsync(string? bearName, string? type)
        {
            var query = GetQueryable();

            if (!string.IsNullOrWhiteSpace(bearName))
            {
                query = query.Where(h => h.BearName.Contains(bearName));
            }

            if (!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(h => h.BearType != null && h.BearName.Contains(bearName));
            }

            query.OrderByDescending(h => h.BearProfileId);

            query.AsNoTracking();

            return await query.ToListAsync();
        }

        //public async Task<List<BearProfile>> SearchNumericalAsync(string? bearName, decimal? price)
        //{
        //    var query = GetQueryable();

        //    if (!string.IsNullOrWhiteSpace(bearName))
        //    {
        //        query = query.Where(h => h.bearName.Contains(bearName));
        //    }

        //    if (price.HasValue)
        //    {
        //        query = query.Where(h => h.Price == price);
        //    }

        //    query.OrderByDescending(h => h.BearProfileId);

        //    query.AsNoTracking();

        //    return await query.ToListAsync();
        //}



        //public async Task<PaginationResult<List<BearProfile>>> SearchWithPaginationAsync(SearchRequestDto request)
        //{
        //    var items = await SearchAsync(request.bearName, request.Material);
        //    var totalItems = items.Count();
        //    var totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize.Value);

        //    items = items.Skip((request.CurrentPage.Value - 1) * request.PageSize.Value).Take(request.PageSize.Value).ToList();

        //    var result = new PaginationResult<List<BearProfile>>
        //    {
        //        TotalItems = totalItems,
        //        TotalPages = totalPages,
        //        CurrentPages = request.CurrentPage.Value,
        //        PageSizes = request.PageSize.Value,
        //        Items = items
        //    };
        //    return result;
        //}
    }
}
