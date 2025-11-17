using Repositories;
using Repositories.Models;
using Repository.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BearProfileService
    {
        private readonly BearProfileRepository _mainRepo;
        private readonly BearTypeRepository _subRepo;
        public BearProfileService()
        {
            _mainRepo = new();
            _subRepo = new();
        }

        public async Task<List<BearProfile>> GetAllAsync() => await _mainRepo.GetAllAsync();

        public async Task<BearProfile?> GetByIdAsync(int id) => await _mainRepo.GetByIdAsync(id);

        public IQueryable<BearProfile> GetQueryable()
        {
            return _mainRepo.GetQueryable();
        }

        public async Task<int> CreateAsync(BearProfile entity)
        {
            var sub = await _subRepo.GetByIdAsync(entity.BearTypeId.Value);
            if (sub == null) throw new KeyNotFoundException($"ID {entity.BearTypeId} is not found !");
            return await _mainRepo.CreateAsync(entity);
        }

        public async Task<int> UpdateAsync(BearProfile entity)
        {
            var sub = await _subRepo.GetByIdAsync(entity.BearTypeId.Value);
            if (sub == null) throw new KeyNotFoundException($"ID {entity.BearTypeId} is not found !");

            return await _mainRepo.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _mainRepo.GetByIdAsync(id);
            if (existing == null)
                return false;

            return await _mainRepo.RemoveAsync(existing);
        }

        public async Task<List<BearProfile>> SearchAsync(string? modelName, string? material, int? stock)
        {
            return await _mainRepo.SearchAsync(modelName, material, stock);
        }

        public async Task<PaginationResult<List<BearProfile>>> SearchWithPaginationAsync(MainSearchRequest request)
        {
            return await _mainRepo.SearchWithPaginationAsync(request);
        }

    }
}
