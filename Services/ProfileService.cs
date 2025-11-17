using Repositories.Basic;
using Repositories.ModelExtensions;
using Repositories.Models;
using System.Linq.Expressions;

namespace Services
{
    public interface IProfileService
    {
        public Task<List<BearProfile>> GetAllAsyncIncludeOrderBy();
        public Task<BearProfile> GetByIdAsyncInclude(int id);
        public Task<BearProfile> CreateAsync(BearProfile entity);
        public Task<BearProfile> UpdateAsync(BearProfile entity);
        public Task<bool> DeleteAsync(int id);
        public Task<PaginationResult<BearProfile>> SearchWithPagingAsyncIncludeOrderBy(
            Expression<Func<BearProfile, bool>> predicate,
            int currentPage,
            int pageSize);
        public Task<int> GetMaxId();
    }

    public class ProfileService : IProfileService
    {
        public readonly GenericRepository<BearProfile> _repository;

        public ProfileService(GenericRepository<BearProfile> repository)
        {
            _repository = repository;
        }

        public async Task<List<BearProfile>> GetAllAsyncIncludeOrderBy()
            => await _repository.GetAllAsyncIncludeOrderBy(orderBy: x => x.BearProfileId, ascending: true);

        public async Task<BearProfile> GetByIdAsyncInclude(int id)
            => await _repository.GetByIdAsyncInclude(id);

        public async Task<BearProfile> CreateAsync(BearProfile entity)
            => await _repository.CreateAsync(entity);

        public async Task<BearProfile> UpdateAsync(BearProfile entity)
            => await _repository.UpdateAsync(entity);

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repository.RemoveAsync(entity);
        }

        public async Task<PaginationResult<BearProfile>> SearchWithPagingAsyncIncludeOrderBy(
            Expression<Func<BearProfile, bool>> predicate, int currentPage, int pageSize)
            => await _repository.SearchWithPagingAsyncIncludeOrderBy(
                predicate: predicate,
                currentPage: currentPage,
                pageSize: pageSize,
                orderBy: x => x.BearProfileId,
                ascending: true
                
            );

        public async Task<int> GetMaxId()
            => await _repository.GetMaxId();
    }
}