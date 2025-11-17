using Microsoft.EntityFrameworkCore;
using PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.Entities;
using PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.ModelExtension;
using PE_PRN232_FA25_TranNguyenVietQuang_BE_Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_TranNguyenVietQuang_BE_Repository.Implementations;

public sealed class BearProfileRepository : PE_PRN232_FA25_TranNguyenVietQuang_BE_Repository.BaseRepository<BearProfile, FA25BearDBContext>, IBearProfileRepository
{
    public BearProfileRepository(PE_PRN232_FA25_TranNguyenVietQuang_BE_Repository.IDbFactory<FA25BearDBContext> dbFactory)
        : base(dbFactory)
    {
    }

    public async Task<List<BearProfile>> SearchAsync(string? bearName, int? bearWeight, string? bearTypeName)
    {
        var query = DbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(bearName))
        {
            query = query.Where(x => x.BearName != null && x.BearName.Contains(bearName));
        }

        if (!string.IsNullOrWhiteSpace(bearWeight.ToString()))
        {
            query = query.Where(x => x.BearWeight != null && x.BearWeight == bearWeight);
        }

        if (!string.IsNullOrWhiteSpace(bearTypeName))
        {
            query = query.Where(x => x.BearType.BearTypeName != null && x.BearType.BearTypeName.Contains(bearTypeName));
        }

        var results = await query.ToListAsync();
        return results.ToList();
    }

    public async Task<PaginationResult<List<BearProfile>>> SearchWithPaginationAsync(BearProfileSearchRequest request)
    {
        var items = await this.SearchAsync(request.BearName, request.BearWeight, request.BearTypeName);
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
    public async Task<List<BearProfile>> GetAllAsync()
    {
        return await DbSet.Include(b => b.BearType).ToListAsync();
    }
}