using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.ModelExtensions;
using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenQuangMinh_BE.Service
{
    public interface IBearProfileService
    {
        Task<List<BearProfile>> GetAllAsync();
        Task<BearProfile> GetByIdAsync(int id);

        Task<List<BearProfile>> SearchAsync(string? BearName, string? BearWeight, string? BearTypeName);

        //Task<PaginationResult<List<BearProfile>>> SearchWithPaginationAsync(BearSearchRequest searchRequest);

        Task<int> CreateAsync(BearProfile bearProfile);
        Task<int> UpdateAsync(BearProfile bearProfile);
        Task<bool> DeleteAsync(int id);
    }
}
