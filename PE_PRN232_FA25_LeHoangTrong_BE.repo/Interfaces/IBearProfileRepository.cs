using PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;
using PE_PRN232_FA25_LeHoangTrong_BE.repo.ModelExtensions;

namespace PE_PRN232_FA25_LeHoangTrong_BE_repo.Interfaces;

public interface IBearProfileRepository : PE_PRN232_FA25_LeHoangTrong_BE_repo.IBaseRepository<BearProfile>
{
    Task<PaginationResult<List<BearProfile>>> SearchAsync(BearSearchRequest request);
}