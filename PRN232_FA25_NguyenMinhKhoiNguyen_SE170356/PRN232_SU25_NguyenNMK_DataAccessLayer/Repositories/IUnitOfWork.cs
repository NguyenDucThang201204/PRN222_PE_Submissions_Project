using PRN232_SU25_NguyenNMK_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_NguyenNMK_DataAccessLayer.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<BearProfile> BearProfileRepository { get; }
        IRepository<BearType> BearTypeRepository { get; }
        IRepository<BearAccount> BearAccountRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
