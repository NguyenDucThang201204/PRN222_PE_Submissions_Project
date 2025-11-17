using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.ModelExtensions;
using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Models;
using System.Linq.Expressions;

namespace PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Interface
{
    public interface IBearProfileRepository
    {
        Task AddAsync(BearProfile entity);
        void Update(BearProfile entity);
        void Delete(BearProfile entity);
        void Delete(int id);
        Task SaveAsync();
        Task<BearProfile?> GetByIdAsync(int id);
        Task<IEnumerable<BearProfile>> GetAllAsync(
            Expression<Func<BearProfile, bool>>? filter = null,
            Func<IQueryable<BearProfile>, IOrderedQueryable<BearProfile>>? orderBy = null);
        IQueryable<BearProfile> GetQueryable();
        Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(BearSearchRequest searchRequest);
    }
}