using Microsoft.EntityFrameworkCore;
using Repositories.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Basic
{
    public class GenericRepository<T> where T : class
    {
        protected SU25BearDBContext _context;

        public GenericRepository()
        {
            _context ??= new SU25BearDBContext();
        }

        public GenericRepository(SU25BearDBContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public async Task<int> CreateAsync(T entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }
        public void Update(T entity)
        {
            _context.ChangeTracker.Clear();
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _context.ChangeTracker.Clear();
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;


            try
            {
                var keyValues = _context.Model.FindEntityType(typeof(T))
                                ?.FindPrimaryKey()
                                ?.Properties
                                ?.Select(p => p.PropertyInfo.GetValue(entity))
                                .ToArray();

                if (keyValues == null || keyValues.Length == 0)
                    throw new InvalidOperationException("No primary key defined for entity.");

                var existingEntity = await _context.Set<T>().FindAsync(keyValues);

                if (existingEntity == null) return 0;

                _context.Entry(existingEntity).State = EntityState.Detached;
                _context.Entry(entity).State = EntityState.Modified;

                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public bool Remove(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            _context.ChangeTracker.Clear();
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T GetById(string code)
        {
            return _context.Set<T>().Find(code);
        }

        public async Task<T> GetByIdAsync(string code)
        {
            return await _context.Set<T>().FindAsync(code);
        }

        /*
        https://guidgenerator.com/
        uniqueidentifier | guid: daacb4fb-ff73-46ef-98f1-4af9aab2a30a
         */
        public T GetById(Guid code)
        {
            return _context.Set<T>().Find(code);
        }

        public async Task<T> GetByIdAsync(Guid code)
        {
            return await _context.Set<T>().FindAsync(code);
        }
    }
}
