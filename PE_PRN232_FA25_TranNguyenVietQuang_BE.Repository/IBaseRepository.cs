using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PE_PRN232_FA25_TranNguyenVietQuang_BE_Repository;

public interface IBaseRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> GetByKeysAsync(object[] keys);
    Task<List<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null);
    DbSet<TEntity> Set();
}