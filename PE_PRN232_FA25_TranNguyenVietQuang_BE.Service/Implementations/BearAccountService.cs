using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PE_PRN232_FA25_TranNguyenVietQuang_BE_Repository.Interfaces;
using PE_PRN232_FA25_TranNguyenVietQuang_BE_Service.Interfaces;
using PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.Entities;

namespace PE_PRN232_FA25_TranNguyenVietQuang_BE_Service.Implementations;

public sealed class BearAccountService : IBearAccountService
{
    private readonly IBearAccountRepository _repository;

    public BearAccountService(IBearAccountRepository repository)
    {
        _repository = repository;
    }

    public Task<List<BearAccount>> GetAllAsync()
        => _repository.GetAllAsync();

    public Task<BearAccount?> GetByKeysAsync(object[] keys)
        => _repository.GetByKeysAsync(keys);

    public Task AddAsync(BearAccount entity)
        => _repository.AddAsync(entity);

    public Task UpdateAsync(BearAccount entity)
        => _repository.UpdateAsync(entity);

    public Task DeleteAsync(BearAccount entity)
        => _repository.DeleteAsync(entity);
    public Task<BearAccount?> GetByUserNameAsync(string? userName)
        => _repository.GetByUserNameAsync(userName);
}