using Bear.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bear.Repositories.Interface
{
    public interface IBearProfileRepository
    {
        Task<int> CreateAsync(BearProfile item);
        Task<int> UpdateAsync(BearProfile item);
        Task<bool> RemoveAsync(BearProfile item);
        Task<BearProfile> GetItemByIdAsync(int id);
        Task<List<BearProfile>> GetAllItemsAsync();
    }
}
