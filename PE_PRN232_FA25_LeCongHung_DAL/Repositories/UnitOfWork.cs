using PE_PRN232_FA25_LeCongHung_DAL.Data;
using PE_PRN232_FA25_LeCongHung_DAL.Entities;

namespace PE_PRN232_FA25_LeCongHung_DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BearDbContext _context;
        private IGenericRepository<BearType>? _bearTypeRepository;
        private IGenericRepository<BearProfile>? _bearProfileRepository;
        private IGenericRepository<BearAccount>? _bearAccountRepository;

        public UnitOfWork(BearDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<BearType> BearTypeRepository
        {
            get
            {
                _bearTypeRepository ??= new GenericRepository<BearType>(_context);
                return _bearTypeRepository;
            }
        }

        public IGenericRepository<BearProfile> BearProfileRepository
        {
            get
            {
                _bearProfileRepository ??= new GenericRepository<BearProfile>(_context);
                return _bearProfileRepository;
            }
        }

        public IGenericRepository<BearAccount> BearAccountRepository
        {
            get
            {
                _bearAccountRepository ??= new GenericRepository<BearAccount>(_context);
                return _bearAccountRepository;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

