using Repositories.Basic;
using Repositories.Models;
using System.Linq.Expressions;

namespace Services
{
    public interface ITypeService
    {
        public Task<List<BearType>> GetAllAsyncOrderBy();
        public Task<BearType> GetByIdAsync(int id);
        public Task<BearType> CreateAsync(BearType entity);
        public Task<BearType> UpdateAsync(BearType entity);
        public Task<bool> DeleteAsync(int id);
        public Task<IEnumerable<BearType>> SearchAsync(Expression<Func<BearType, bool>> predicate);
    }

    public class TypeService : ITypeService
    {
        public readonly GenericRepository<BearType> _repository;

        public TypeService(GenericRepository<BearType> repository)
        {
            _repository = repository;
        }

        public async Task<List<BearType>> GetAllAsyncOrderBy()
            => await _repository.GetAllAsyncOrderBy(x => x.BearTypeId, true);

        public async Task<BearType> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task<BearType> CreateAsync(BearType entity)
            => await _repository.CreateAsync(entity);

        public async Task<BearType> UpdateAsync(BearType entity)
            => await _repository.UpdateAsync(entity);

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repository.RemoveAsync(entity);
        }

        public async Task<IEnumerable<BearType>> SearchAsync(Expression<Func<BearType, bool>> predicate)
            => await _repository.SearchAsync(predicate);
    }
}
