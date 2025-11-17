using Repository.Models;

namespace Service
{
    public interface IItemService
    {
        Task<BearProfile> AddAsync(BearProfile item);
        Task<bool> DeleteAsync(int id);
        Task<List<BearProfile>> GetAll();
        Task<BearProfile> GetByIdAsync(int id);
        Task<List<BearProfile>> Search(string bearName, double? weight, string? bearTypeName);
        Task<BearProfile> UpdateAsynce(BearProfile item);
    }
}