using PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.Entities;
using PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.ModelExtension;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_TranNguyenVietQuang_BE_Repository.Interfaces;

public interface IBearProfileRepository : PE_PRN232_FA25_TranNguyenVietQuang_BE_Repository.IBaseRepository<BearProfile>
{
    Task<List<BearProfile>> SearchAsync(string? bearName, int? bearWeight, string? bearTypeName);
    Task<PaginationResult<List<BearProfile>>> SearchWithPaginationAsync(BearProfileSearchRequest request);
}