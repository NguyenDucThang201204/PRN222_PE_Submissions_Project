using Model.DTOs;

namespace Service
{
    public interface IBearService
    {
        Task<List<BearDto>> GetAllBear();

        Task<BearDto?> GetByIdAsync(int id);

        Task<bool> DeleteByIdAsync(int id);
    }
}
