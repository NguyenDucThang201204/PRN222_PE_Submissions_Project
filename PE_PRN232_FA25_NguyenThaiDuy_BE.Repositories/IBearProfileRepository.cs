using PE_PRN232_FA25_NguyenThaiDuy_BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenThaiDuy_BE.Repositories
{
    public interface IBearProfileRepository
    {
        Task<IEnumerable<BearProfile>> GetAllAsync();

        Task<BearProfile?> GetByIdAsync(int id);

        Task<BearProfile> CreateAsync(BearProfileDto dto);

        Task<bool> UpdateAsync(int id, BearProfileDto dto);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<BearProfile>> SearchAsync(string? bearName, double? bearWeight);
    }
}
