using Repositories;
using Repositories.Models;
using Repositories.ModelExtensions;

namespace Services
{
    public class BearProfileService
    {
        private readonly BearProfileRepository _repo;
        private readonly BearTypeRepository _subRepo;
        public BearProfileService(BearProfileRepository repo, BearTypeRepository subRepo)
        {
            _repo = repo;
            _subRepo = subRepo;
        }
        public async Task<List<BearProfile>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<BearProfile?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public IQueryable<BearProfile> GetQueryable()
        {
            return _repo.GetQueryable();
        }

        public async Task<List<BearProfile>> SearchAsync(string? bearName, string? type)
        {
            return await _repo.SearchAsync(bearName, type);
        }

        //public async Task<PaginationResult<List<BearProfile>>> SearchWithPaginationAsync(SearchRequestDto request)
        //{
        //    return await _repo.SearchWithPaginationAsync(request);
        //}

        public async Task<int> CreateAsync(BearProfile entity)
        {
            var subEntity = await _subRepo.GetByIdAsync(entity.BearTypeId);
            if (subEntity == null)
                throw new KeyNotFoundException($"Brand with ID {entity.BearTypeId} not found.");

            return await _repo.CreateAsync(entity);
        }

        public async Task<int> UpdateAsync(BearProfile entity)
        {
            if(entity.BearProfileId <= 0)
                throw new ArgumentException("Invalid BearProfile ID.");
            var subEntity = await _subRepo.GetByIdAsync(entity.BearTypeId);
            if (subEntity == null)
                throw new KeyNotFoundException($"Brand with ID {entity.BearTypeId} not found.");

             return await _repo.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return false;

            return await _repo.RemoveAsync(existing);
        }
    }
}
