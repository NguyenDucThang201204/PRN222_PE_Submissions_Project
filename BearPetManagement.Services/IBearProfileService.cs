using BearPetManagement.Repositories.ModelExtensions;
using BearPetManagement.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearPetManagement.Services
{
    public interface IBearProfileService
    {
        Task<List<BearProfile>> GetAllAsync();
        Task<BearProfile> GetByIdAsync(int id);
        Task<int> CreateAsync(BearProfileDTO bearProfile);
        Task<int> UpdateAsync(BearProfileDTO bearProfile);
        Task<bool> DeleteAsync(int id);
    }
}
