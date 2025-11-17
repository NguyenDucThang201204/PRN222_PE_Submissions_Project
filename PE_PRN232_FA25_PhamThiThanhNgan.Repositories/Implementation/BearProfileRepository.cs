using Microsoft.EntityFrameworkCore;
using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Interface;
using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.ModelExtensions;
using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Models;
using System.Linq;
using System.Linq.Expressions;

namespace Practice_Fa25_PE_Repositories.Repository
{
    public class BearProfileRepository : IBearProfileRepository
    {
        private readonly Fa25bearDbContext _context;
        private readonly DbSet<BearProfile> _dbSet;

        public BearProfileRepository(Fa25bearDbContext context)
        {
            _context = context;
            _dbSet = context.Set<BearProfile>();
        }

        public async Task<BearProfile?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(p => p.BearType)
                .FirstOrDefaultAsync(p => p.BearProfileId == id);
        }

        public async Task<IEnumerable<BearProfile>> GetAllAsync(
            Expression<Func<BearProfile, bool>>? filter = null,
            Func<IQueryable<BearProfile>, IOrderedQueryable<BearProfile>>? orderBy = null)
        {
            IQueryable<BearProfile> query = _dbSet.Include(p => p.BearType);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public IQueryable<BearProfile> GetQueryable()
        {
            return _dbSet.Include(p => p.BearType).AsQueryable();
        }

        public async Task AddAsync(BearProfile entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(BearProfile entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(BearProfile entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public void Delete(int id)
        {
            BearProfile? entityToDelete = _dbSet.Find(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<BearProfile>> SearchAsync(string bearName, double bearWeight, string typeName)
        {
            var items = await _context.BearProfiles
                 .Include(c => c.BearType)
                 .Where(c =>
                 (string.IsNullOrEmpty(bearName) || c.BearName.Contains(bearName))
                 && (bearWeight == 0 || c.Weight == bearWeight)
                 && (string.IsNullOrEmpty(typeName) || c.BearType.BearTypeName.Contains(typeName))
                 ).OrderByDescending(c => c.ModifiedDate)
                 .ToListAsync();
            return items ?? new List<BearProfile>();
        }

        public async Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(BearSearchRequest searchRequest)
        {
            var weight = searchRequest.BearWeigth ?? 0;
            var pageSize = searchRequest.PageSize ?? 10;
            var currentPage = searchRequest.CurrentPage ?? 1;

            var items = await this.SearchAsync(searchRequest.BearName, weight, searchRequest.BearTypeName);
            var totalItems = items.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            items = items.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<BearProfile>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = items
            };
            return result ?? new PaginationResult<List<BearProfile>>();
        }
    }
}