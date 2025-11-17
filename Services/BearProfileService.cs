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
    public class BearProfileService
    {
        private readonly BearProfileRepository _repository;
        public BearProfileService()
        {
            _repository = new BearProfileRepository();
        }

        public async Task<int> CreateAsync(BearProfile bearProfile)
        {
            try
            {
                return await _repository.CreateAsync(bearProfile);
            }
            catch (Exception ex)
            {

            }
            return 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var item = await _repository.GetByIdAsync(id);
                if (item != null)
                {
                    return await _repository.RemoveAsync(item);
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public async Task<List<BearProfile>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BearProfile> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(string? bearName, double? bearWeight, string? bearTypeName, int page = 1, int pageSize = 10)
        {
            try
            {
                var list = await _repository.SearchAsync(bearName, bearWeight, bearTypeName);
                var items = list
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                return new PaginationResult<List<BearProfile>>
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling((double)list.Count / pageSize),
                    TotaItems = list.Count,
                    Items = items
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<BearProfile>> SearchAsync(string? bearProfile, double? weight, string? bearTypeName)
        {
            try
            {
                return await _repository.SearchAsync(bearProfile, weight, bearTypeName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(BearProfile bearProfile)
        {
            try
            {
                return await _repository.UpdateAsync(bearProfile);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
