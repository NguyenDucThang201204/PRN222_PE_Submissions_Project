using Microsoft.EntityFrameworkCore;
using PRN232_SU25_NguyenNMK_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_NguyenNMK_DataAccessLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FA25BearDBContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(FA25BearDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(BearProfile))
            {
                return await _context.Set<BearProfile>().Include(h => h.BearType).ToListAsync() as IEnumerable<T>;
            }
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(BearProfile))
            {
                return await _context.Set<BearProfile>().Include(e => e.BearType)
                    .FirstOrDefaultAsync(h => EF.Property<int>(h, "BearProfileId") == id) as T;
            }
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            if (typeof(T) == typeof(BearProfile))
            {
                // Convert predicate to Expression<Func<BearProfile, bool>>
                var parameter = Expression.Parameter(typeof(BearProfile), "b");
                var body = new ParameterReplacer(parameter).Visit(predicate.Body);
                var lambda = Expression.Lambda<Func<BearProfile, bool>>(body, parameter);

                return await _context.Set<BearProfile>().Include(h => h.BearType)
                    .Where(lambda).ToListAsync() as IEnumerable<T>;
            }
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }

    // Helper class to replace parameter in Expression
    public class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        public ParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _parameter;
        }
    }
}
