using PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;
using PE_PRN232_FA25_LeHoangTrong_BE_repo.Interfaces;
using PE_PRN232_FA25_LeHoangTrong_BE_service.Interfaces;

namespace PE_PRN232_FA25_LeHoangTrong_BE_service.Implementations;

public sealed class BearAccountService : IBearAccountService
{
    private readonly IBearAccountRepository _repository;

    public BearAccountService(IBearAccountRepository repository)
    {
        _repository = repository;
    }

    public Task<BearAccount?> GetByEmailAsync(string? email)
        => _repository.GetByEmailAsync(email);
}