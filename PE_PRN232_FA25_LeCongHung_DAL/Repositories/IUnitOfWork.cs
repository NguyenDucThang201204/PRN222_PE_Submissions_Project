using PE_PRN232_FA25_LeCongHung_DAL.Entities;

namespace PE_PRN232_FA25_LeCongHung_DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<BearType> BearTypeRepository { get; }
        IGenericRepository<BearProfile> BearProfileRepository { get; }
        IGenericRepository<BearAccount> BearAccountRepository { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}

