using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PE_PRN232_FA25_LeHoangTrong_BE_repo;

public abstract class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
    where TEntity : class
    where TContext : DbContext
{
    private readonly IDbFactory<TContext> _dbFactory;
    private DbSet<TEntity>? _dbSet;

    protected BaseRepository(IDbFactory<TContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    protected TContext DbContext => _dbFactory.DbContext;

    protected DbSet<TEntity> DbSet => _dbSet ??= DbContext.Set<TEntity>();

    public virtual DbSet<TEntity> Set() => DbSet;

    public virtual async Task<TEntity?> GetByKeysAsync(object[] keys)
        => await DbSet.FindAsync(keys);

    public virtual async Task<List<TEntity>> GetAllAsync()
        => await DbSet.ToListAsync();

    public virtual async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        await DbContext.SaveChangesAsync();
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
        await DbContext.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await DbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        await DbContext.SaveChangesAsync();
    }

    public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null)
        => predicate is null ? DbSet : DbSet.Where(predicate);
}