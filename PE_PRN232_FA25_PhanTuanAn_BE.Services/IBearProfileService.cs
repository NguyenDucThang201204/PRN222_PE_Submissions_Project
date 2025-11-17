using PE_PRN232_FA25_PhanTuanAn_BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_PhanTuanAn_BE.Services
{
    public interface IBearProfileService
    {
        Task<List<BearProfile>> GetAllAsync();
        Task<BearProfile> GetByIdAsync(int id);

        Task<int> CreateAsync(BearProfile bear);
        Task<int> UpdateAsync(BearProfile bear);
        Task<bool> DeleteAsync(int id);
    }
}
