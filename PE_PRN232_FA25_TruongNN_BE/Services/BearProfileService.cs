using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositores;
using Repositores.Models;
using Repositories.ModelExtensions;

namespace Services
{
    public class BearProfileService
    {
        private readonly BearProfileRepository _repository;
        public BearProfileService(BearProfileRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> CreateAsync(BearProfile BearProfile)
        {
            try
            {
                return await _repository.CreateAsync(BearProfile);
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

        public async Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(string? BearName, double? weight, int page = 1, int pageSize = 10)
        {
            try
            {
                var list = await _repository.SearchAsync(BearName, weight);
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
        public async Task<List<BearProfile>> SearchAsync(string? BearName, double? weight)
        {
            try
            {
                return await _repository.SearchAsync(BearName, weight);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(BearProfile BearProfile)
        {
            try
            {
                return await _repository.UpdateAsync(BearProfile);
            }
            catch (Exception ex)
            {
                throw;
            }
            return 0;
        }
    }
}
