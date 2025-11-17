using Repositories.ThienNH;
using Repositories.ThienNH.ModelExtensions;
using Repositories.ThienNH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ThienNH
{
    public class BearProfileService : IBearProfileService
    {
        private readonly BearProfileRepository _repository;
        public BearProfileService() => _repository = new BearProfileRepository();
        public async Task<int> CreateAsync(BearProfile bearProfile)
        {

            try
            {
                return await _repository.CreateAsync(bearProfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                if (ex.InnerException != null)
                    Console.WriteLine("InnerException: " + ex.InnerException.ToString());
                throw;
            }
            ;
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
            catch (Exception ex) { }
            ;
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
            catch (Exception ex) { }
            return new BearProfile();
        }

        public async Task<List<BearProfile>> SearchAsync(string bearName, double weight, string bearTypeName)
        {
            try
            {
                return await _repository.SearchAsync(bearName, weight, bearTypeName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new List<BearProfile>();
        }

        public async Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(BearProfileSearchRequest searchRequest)
        {
            try
            {
                return await _repository.SearchWithPagingAsync(searchRequest);
            }
            catch (Exception ex) { }
            ;
            return new PaginationResult<List<BearProfile>>();
        }

        public async Task<int> UpdateAsync(BearProfile bearProfile)
        {
            try
            {
                return await _repository.UpdateAsync(bearProfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                if (ex.InnerException != null)
                    Console.WriteLine("InnerException: " + ex.InnerException.ToString());
                throw;
            }
            ;
            return 0;
        }
    }
}
