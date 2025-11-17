using PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;
using PE_PRN232_FA25_LeHoangTrong_BE.repo.ModelExtensions;
using PE_PRN232_FA25_LeHoangTrong_BE_repo.Interfaces;
using PE_PRN232_FA25_LeHoangTrong_BE_service.Interfaces;

namespace PE_PRN232_FA25_LeHoangTrong_BE_service.Implementations;

public sealed class BearProfileService : IBearProfileService
{
    private readonly IBearProfileRepository _repository;

    public BearProfileService(IBearProfileRepository repository)
    {
        _repository = repository;
    }

    public Task<List<BearProfile>> GetAllAsync()
        => _repository.GetAllAsync();

    public Task<BearProfile?> GetByKeysAsync(object[] keys)
        => _repository.GetByKeysAsync(keys);

    public Task AddAsync(BearProfile entity)
        => _repository.AddAsync(entity);

    public Task UpdateAsync(BearProfile entity)
        => _repository.UpdateAsync(entity);

    public Task DeleteAsync(BearProfile entity)
        => _repository.DeleteAsync(entity);

    public Task<PaginationResult<List<BearProfile>>> SearchAsync(BearSearchRequest request)
        => _repository.SearchAsync(request);
}