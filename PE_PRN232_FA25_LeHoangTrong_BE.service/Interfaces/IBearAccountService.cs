using PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;

namespace PE_PRN232_FA25_LeHoangTrong_BE_service.Interfaces;

public interface IBearAccountService
{
    Task<BearAccount?> GetByEmailAsync(string? email);

}