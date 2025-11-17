using Model.DTOs;
using Repository;

namespace Service
{
    public class BearService : IBearService
    {
        private readonly IBearRepo _bearRepo;

        public BearService(IBearRepo bear)
        {
            _bearRepo = bear;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _bearRepo.DeleteAsync(id); ;
        }

        public async Task<List<BearDto>> GetAllBear()
        {
            return await _bearRepo.GetAllAsync();
        }

        public async Task<BearDto?> GetByIdAsync(int id)
        {
            return await _bearRepo.GetByIdAsync(id);
        }
    }
}
