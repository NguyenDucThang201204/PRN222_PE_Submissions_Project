using Model;
using Model.DTOs;

namespace Repository
{
    public interface IBearRepo
    {
        Task<List<BearDto>> GetAllAsync();

        Task<BearDto?> GetByIdAsync(int id);

        Task<BearProfile?> GetByIdAsyncNoDto(int id);

        Task<BearDto?> UpdateAsync(BearProfile bear);

        Task<BearDto?> CreateAsync(BearProfile bear);

        Task<bool> DeleteAsync(int id);
    }
}
