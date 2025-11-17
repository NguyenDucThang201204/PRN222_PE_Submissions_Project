using PRN232_SU25_NguyenNMK_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_NguyenNMK_DataAccessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FA25BearDBContext _context;
        public IRepository<BearProfile> BearProfileRepository { get; private set; }
        public IRepository<BearType> BearTypeRepository { get; private set; }
        public IRepository<BearAccount> BearAccountRepository { get; private set; }

        public UnitOfWork(FA25BearDBContext context)
        {
            _context = context;
            BearProfileRepository = new Repository<BearProfile>(context);
            BearTypeRepository = new Repository<BearType>(context);
            BearAccountRepository = new Repository<BearAccount>(context);
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
