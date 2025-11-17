using Repositories;
using Repositories.ModelExtensions;
using Repositories.Models;

namespace Services
{
    public class MainService
    {
        private readonly MainRepository _repository;

        public MainService()
        {
            _repository ??= new MainRepository();
        }

        public async Task<int> CreateAsync(BearProfile item)
        {
            return await _repository.CreateAsync(item);
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

        public Task<BearProfile> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<List<BearProfile>> SearchAsync(string BearName, double BearWeight, string BearTypeName)
        {
            return _repository.SearchAsync(BearName, BearWeight, BearTypeName);
        }

        public async Task<PaginationResult<List<BearProfile>>> SearchPagingAsync(MainSearchRequest searchRequest)
        {
            return await _repository.SearchPagingAsync(searchRequest);
        }

        public async Task<int> UpdateAsync(BearProfile item)
        {
            return await _repository.UpdateAsync(item);
        }
    }
}
