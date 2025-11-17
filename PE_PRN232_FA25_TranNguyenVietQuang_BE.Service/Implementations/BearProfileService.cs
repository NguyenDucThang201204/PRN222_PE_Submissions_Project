using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PE_PRN232_FA25_TranNguyenVietQuang_BE_Repository.Interfaces;
using PE_PRN232_FA25_TranNguyenVietQuang_BE_Service.Interfaces;
using PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.Entities;
using PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.ModelExtension;

namespace PE_PRN232_FA25_TranNguyenVietQuang_BE_Service.Implementations;

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

    public Task<List<BearProfile>> SearchAsync(string? bearName, int? bearWeight, string? bearTypeName)
        => _repository.SearchAsync(bearName, bearWeight, bearTypeName);

    public async Task<PaginationResult<List<BearProfile>>> SearchWithPaginationAsync(BearProfileSearchRequest request)
    {
        return await _repository.SearchWithPaginationAsync(request);
    }
}