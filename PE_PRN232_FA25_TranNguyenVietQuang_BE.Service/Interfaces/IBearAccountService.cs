using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.Entities;

namespace PE_PRN232_FA25_TranNguyenVietQuang_BE_Service.Interfaces;

public interface IBearAccountService
{
    Task<List<BearAccount>> GetAllAsync();
    Task<BearAccount?> GetByKeysAsync(object[] keys);
    Task AddAsync(BearAccount entity);
    Task UpdateAsync(BearAccount entity);
    Task DeleteAsync(BearAccount entity);    Task<BearAccount?> GetByUserNameAsync(string? userName);

}