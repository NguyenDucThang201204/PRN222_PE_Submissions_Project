using PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;

namespace PE_PRN232_FA25_LeHoangTrong_BE_repo.Interfaces;

public interface IBearAccountRepository : PE_PRN232_FA25_LeHoangTrong_BE_repo.IBaseRepository<BearAccount>
{    Task<BearAccount?> GetByEmailAsync(string? email);

}