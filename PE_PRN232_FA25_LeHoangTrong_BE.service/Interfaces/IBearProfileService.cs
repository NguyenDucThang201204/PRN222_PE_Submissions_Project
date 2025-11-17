using PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;
using PE_PRN232_FA25_LeHoangTrong_BE.repo.ModelExtensions;

namespace PE_PRN232_FA25_LeHoangTrong_BE_service.Interfaces;

public interface IBearProfileService
{
    Task<List<BearProfile>> GetAllAsync();
    Task<BearProfile?> GetByKeysAsync(object[] keys);
    Task AddAsync(BearProfile entity);
    Task UpdateAsync(BearProfile entity);
    Task DeleteAsync(BearProfile entity);
    Task<PaginationResult<List<BearProfile>>> SearchAsync(BearSearchRequest request);
}