using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;

namespace PE_PRN232_FA25_LeHoangTrong_BE_service.Interfaces;

public interface IBearTypeService
{
    Task<List<BearType>> GetAllAsync();
    Task<BearType?> GetByKeysAsync(object[] keys);
    Task AddAsync(BearType entity);
    Task UpdateAsync(BearType entity);
    Task DeleteAsync(BearType entity);
}