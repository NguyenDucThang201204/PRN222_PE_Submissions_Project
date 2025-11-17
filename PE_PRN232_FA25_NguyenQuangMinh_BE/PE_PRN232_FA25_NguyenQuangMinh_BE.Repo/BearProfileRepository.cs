using Microsoft.EntityFrameworkCore;
using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.Basic;
using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.ModelExtensions;
using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenQuangMinh_BE.Repo
{
    public class BearProfileRepository : GenericRepository<BearProfile>
    {
        public BearProfileRepository() => _context ??= new DBContext.FA25BearDBContext();
        public BearProfileRepository(DBContext.FA25BearDBContext context) => _context = context;

        public async Task<List<BearProfile>> GetAllAsync() 
        { 
           var a = await _context.BearProfiles.Include(bp => bp.BearType).ToListAsync();
           return a ?? new List<BearProfile>();
        }

        public async Task<BearProfile?> GetByIdAsync(int id)
        {
            var a = await _context.BearProfiles
                .Include(bp => bp.BearType)
                .FirstOrDefaultAsync(bp => bp.BearProfileId == id);
            return a ?? new BearProfile();
        }

        public async Task<List<BearProfile>> SearchAsync(string? BearName, string? BearWeight, string? BearTypeName)
        {
            var a = await _context.BearProfiles
                .Include(d => d.BearType)
                .Where(d =>
                    (d.BearName.Contains(BearName) || string.IsNullOrEmpty(BearName))
                    && (BearWeight == null || d.BearWeight == BearWeight)
                    && (d.BearType.BearTypeName.Contains(BearTypeName) || string.IsNullOrEmpty(BearTypeName))
                )
                .ToListAsync();
            return a ?? new List<BearProfile>();
        }

        //public async Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(BearSearchRequest searchRequest)
        //{
        //    var stockQuantityToSearch = searchRequest.stockQuantity == 0 ? null : searchRequest.stockQuantity;
        //    var a = await this.SearchAsync(searchRequest.ModelName ?? string.Empty, stockQuantityToSearch, searchRequest.FeatureName ?? string.Empty);

        //    var totalItems = a.Count();
        //    var totalPages = (int)Math.Ceiling((double)totalItems / searchRequest.PageSize.Value);

        //    a = a.Skip((searchRequest.CurrentPage.Value - 1) * searchRequest.PageSize.Value).Take(searchRequest.PageSize.Value).ToList();

        //    var result = new PaginationResult<List<VehiclesDatPht>>
        //    {
        //        TotalItems = totalItems,
        //        TotalPages = totalPages,
        //        CurrentPage = searchRequest.CurrentPage.Value,
        //        PageSize = searchRequest.PageSize.Value,
        //        Items = a
        //    };
        //    return result ?? new PaginationResult<List<VehiclesDatPht>>();
        //}

    }
}
