using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.Entities;

namespace PE_PRN232_FA25_TranNguyenVietQuang_BE_Service.Interfaces;

public interface IBearTypeService
{
    Task<List<BearType>> GetAllAsync();
    Task<BearType?> GetByKeysAsync(object[] keys);
    Task AddAsync(BearType entity);
    Task UpdateAsync(BearType entity);
    Task DeleteAsync(BearType entity);
}