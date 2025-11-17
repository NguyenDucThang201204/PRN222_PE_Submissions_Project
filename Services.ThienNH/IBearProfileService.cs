using Repositories.ThienNH.ModelExtensions;
using Repositories.ThienNH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ThienNH
{
    public interface IBearProfileService
    {
        Task<List<BearProfile>> GetAllAsync();
        Task<BearProfile> GetByIdAsync(int id);

        Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(BearProfileSearchRequest searchRequest);
        Task<int> CreateAsync(BearProfile bearProfile);
        Task<int> UpdateAsync(BearProfile bearProfile);
        Task<bool> DeleteAsync(int id);
        Task<List<BearProfile>> SearchAsync(string bearName, double weight, string bearTypeName);
    }
}
