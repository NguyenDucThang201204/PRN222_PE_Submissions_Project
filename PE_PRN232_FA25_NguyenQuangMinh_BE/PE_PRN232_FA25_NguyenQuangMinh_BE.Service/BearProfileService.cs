using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo;
using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.ModelExtensions;
using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenQuangMinh_BE.Service
{
    public class BearProfileService : IBearProfileService
    {
        private readonly BearProfileRepository _repository;

        public BearProfileService() => _repository = new BearProfileRepository();

        public async Task<int> CreateAsync(BearProfile bearProfile)
        {
            return await _repository.CreateAsync(bearProfile);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var a = await _repository.GetByIdAsync(id);
            return await _repository.RemoveAsync(a);
        }

        public Task<List<BearProfile>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public async Task<BearProfile> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<BearProfile>> SearchAsync(string? BearName, string? BearWeight, string? BearTypeName)
        {
            return await _repository.SearchAsync(BearName, BearWeight, BearTypeName);
        }
        //public async Task<PaginationResult<List<BearProfile>>> SearchWithPaginationAsync(BearSearchRequest searchRequest)
        //{
        //    return await _repository.SearchWithPagingAsync(searchRequest);
        //}

        public async Task<int> UpdateAsync(BearProfile bearProfile)
        {
            return await _repository.UpdateAsync(bearProfile);
        }
    }
}
