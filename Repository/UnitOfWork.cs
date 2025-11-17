using Repository.Models;

namespace Repository
{
    public class UnitOfWork
    {
        private readonly FA25BearDBContext _dbContext;
        public UnitOfWork(FA25BearDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public GenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_dbContext);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
