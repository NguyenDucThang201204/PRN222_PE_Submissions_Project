using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PE_PRN232_FA25_LeHoangTrong_BE_repo.Interfaces;
using PE_PRN232_FA25_LeHoangTrong_BE_service.Interfaces;
using PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;

namespace PE_PRN232_FA25_LeHoangTrong_BE_service.Implementations;

public sealed class BearTypeService : IBearTypeService
{
    private readonly IBearTypeRepository _repository;

    public BearTypeService(IBearTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<List<BearType>> GetAllAsync()
        => _repository.GetAllAsync();

    public Task<BearType?> GetByKeysAsync(object[] keys)
        => _repository.GetByKeysAsync(keys);

    public Task AddAsync(BearType entity)
        => _repository.AddAsync(entity);

    public Task UpdateAsync(BearType entity)
        => _repository.UpdateAsync(entity);

    public Task DeleteAsync(BearType entity)
        => _repository.DeleteAsync(entity);
}