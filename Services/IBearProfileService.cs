using Repositories.ModelExtensions;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBearProfileService
    {
        Task<List<BearProfile>> GetAll();

        Task<BearProfile?> GetById(int id);

        Task<int> Create(BearProfile create);

        Task<int> Update(BearProfile update);

        Task<bool> Delete(int id);

        Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(string bearName, int bearWeight, string bearTypeName, int currentPage, int pageSize);

    }
}
