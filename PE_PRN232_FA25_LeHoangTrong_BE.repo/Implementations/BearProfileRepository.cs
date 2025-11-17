using Microsoft.EntityFrameworkCore;
using PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;
using PE_PRN232_FA25_LeHoangTrong_BE.repo.ModelExtensions;
using PE_PRN232_FA25_LeHoangTrong_BE_repo.Interfaces;

namespace PE_PRN232_FA25_LeHoangTrong_BE_repo.Implementations;

public sealed class BearProfileRepository : PE_PRN232_FA25_LeHoangTrong_BE_repo.BaseRepository<BearProfile, FA25BearDB>, IBearProfileRepository
{
    public BearProfileRepository(PE_PRN232_FA25_LeHoangTrong_BE_repo.IDbFactory<FA25BearDB> dbFactory)
        : base(dbFactory)
    {
    }

    public async Task<BearProfile?> GetByKeysAsync(object[] keys)
    {
        if (keys.Length == 0 || keys[0] is not int bearProfileId)
        {
            throw new ArgumentException("Invalid keys provided. Expected an array with a single integer key.", nameof(keys));
        }

        return await DbSet.Include(b => b.BearType).FirstOrDefaultAsync(b => b.BearProfileId == bearProfileId);
    }

    public async Task<PaginationResult<List<BearProfile>>> SearchAsync(BearSearchRequest request)
    {

        var pageSize = request.PageSize > 0 ? request.PageSize.Value : 10;
        var currentPage = request.CurrentPage > 0 ? request.CurrentPage.Value : 1;

        var query = DbContext.BearProfiles
            .Include(b => b.BearType)
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.BearName))
        {
            query = query.Where(b => b.BearName != null && b.BearName.Contains(request.BearName));
        }

        if (request.BearWeight.HasValue && request.BearWeight > 0)
        {
            query = query.Where(b => b.BearWeight >= request.BearWeight);
        }

        if (!string.IsNullOrEmpty(request.BearTypeName))
        {
            query = query.Where(b => b.BearType != null &&
                                    b.BearType.BearTypeName != null &&
                                    b.BearType.BearTypeName.Contains(request.BearTypeName));
        }

        query = query.OrderByDescending(b => b.ModifiedDate);

        var items = await query
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalItems = await query.CountAsync();
        var result = new PaginationResult<List<BearProfile>>
        {
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
            CurrentPage = currentPage,
            PageSize = pageSize,
            Items = items
        };

        return result;
    }
}