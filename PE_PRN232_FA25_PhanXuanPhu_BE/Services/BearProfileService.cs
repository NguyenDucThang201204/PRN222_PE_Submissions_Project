using Repositories;
using Repositories.ModelExtensions;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BearProfileService
    {
        private readonly BearProfileRepository _repo;

        public BearProfileService()
        {
            _repo = new BearProfileRepository();
        }

        public async Task<List<BearProfile>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<BearProfile> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(BearProfile item)
        {
            return await _repo.CreateAsync(item);
        }

        public async Task<int> UpdateAsync(BearProfile item)
        {
            return await _repo.UpdateAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item != null)
            {
                return await _repo.RemoveAsync(item);
            }
            return false;
        }

        public async Task<List<BearProfile>> SearchAsync(string? BearName, double? BearWeight)
        {
            return await _repo.SearchAsync(BearName, BearWeight);
        }

        public async Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(BearProfileRequest searchRequest)
        {
            return await _repo.SearchWithPagingAsync(searchRequest);
        }
    }
}
