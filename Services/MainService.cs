using Repos;
using Repos.ModelExtensions;
using Repos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MainService
    {
        private readonly MainRepo _repository;

        public MainService() => _repository = new MainRepo();

        public async Task<int> CreateAsync(BearProfile bearProfile)
        {
            return await _repository.CreateAsync(bearProfile);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var item = await _repository.GetByIdAsync(id);
                return await _repository.RemoveAsync(item);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<BearProfile>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving items: {ex.Message}");
            }
        }

        public async Task<BearProfile> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<BearProfile>> SearchAsync(string bearName, decimal? bearWeight, string bearTypeName)
        {
            return await _repository.SearchAsync(bearName, bearWeight, bearTypeName);
        }

        public async Task<PaginationResult<List<BearProfile>>> SearchWithPaginationAsync(MainSearchRequest searchRequest)
        {
            return await _repository.SearchWithPagingAsync(searchRequest);
        }

        public async Task<int> UpdateAsync(BearProfile bearProfile)
        {
            return await _repository.UpdateAsync(bearProfile);
        }
    }
}


