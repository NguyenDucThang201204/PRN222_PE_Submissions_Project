using PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.Entities;
using PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.ModelExtension;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_TranNguyenVietQuang_BE_Service.Interfaces;

public interface IBearProfileService
{
    Task<List<BearProfile>> GetAllAsync();
    Task<BearProfile?> GetByKeysAsync(object[] keys);
    Task AddAsync(BearProfile entity);
    Task UpdateAsync(BearProfile entity);
    Task DeleteAsync(BearProfile entity);
    Task<List<BearProfile>> SearchAsync(string? bearName, int? bearWeight, string? bearTypeName);
    Task<PaginationResult<List<BearProfile>>> SearchWithPaginationAsync(BearProfileSearchRequest request);

}