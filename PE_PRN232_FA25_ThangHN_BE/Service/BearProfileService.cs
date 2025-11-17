using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.ModelExtensions;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BearProfileService
    {
        private readonly BearProfileRepository _repo;
        private readonly BearTypeRepository _typeRepo;
        public BearProfileService(BearProfileRepository repo, BearTypeRepository typeRepo)
        {
            _repo = repo;
            _typeRepo = typeRepo;
        }
        public async Task<List<BearProfile>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<BearProfile?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);


        public async Task<List<BearProfile>> SearchAsync(string? bearName, double? bearWeight, string? bearTypeName)
        {
            return await _repo.SearchAsync(bearName, bearWeight, bearTypeName);
        }

        public async Task<PaginationResult<List<BearProfile>>> SearchWithPaginationAsync(SearchRequestDto request)
        {
            return await _repo.SearchWithPaginationAsync(request);
        }

        public async Task<int> CreateAsync(BearProfile entity)
        {
            var subEntity = await _typeRepo.GetByIdAsync(entity.BearTypeId ?? null);
            if (subEntity == null)
                throw new KeyNotFoundException($"Type with ID {entity.BearTypeId} not found.");

            return await _repo.CreateAsync(entity);
        }

        public async Task<int> UpdateAsync(BearProfile entity)
        {
            var subEntity = await _typeRepo.GetByIdAsync(entity.BearTypeId ?? null);
            if (subEntity == null)
                throw new KeyNotFoundException($"Type with ID {entity.BearTypeId} not found.");

            return await _repo.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return false;

            return await _repo.RemoveAsync(existing);
        }
        public IQueryable<BearProfile> GetQueryable()
        {
            return _repo.GetQueryable();
        }
    }
}
