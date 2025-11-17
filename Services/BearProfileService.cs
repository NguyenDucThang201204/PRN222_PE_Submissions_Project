using Repositories;
using Repositories.ModelExtensions;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BearProfileService : IBearProfileService
    {
        private readonly BearProfileRepository _repo;

        public BearProfileService() => _repo ??= new BearProfileRepository();

        public async Task<int> Create(BearProfile create)
        {
            return await _repo.CreateAsync(create);
        }

        public async Task<bool> Delete(int id)
        {
            var delete = await _repo.GetByIdAsync(id);
            return await _repo.RemoveAsync(delete);
        }

        public async Task<BearProfile?> GetById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<List<BearProfile>> GetAll()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<int> Update(BearProfile update)
        {
            return await _repo.UpdateAsync(update);
        }

        public async Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(string bearName, int bearWeight, string bearTypeName, int currentPage, int pageSize)
        {
            return await _repo.SearchWithPagingAsync(bearName, bearWeight, bearTypeName, currentPage, pageSize);
        }
    }
}
