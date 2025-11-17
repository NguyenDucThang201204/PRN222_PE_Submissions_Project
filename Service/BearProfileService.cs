using Repository;
using Repository.ModelExtensions;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BearProfileService
    {
        private readonly BearProfileRepository _repo;
        public BearProfileService()
        {
            _repo = new BearProfileRepository();
        }

        public async Task<int> CreateAsync(BearProfile profile)
        {
            try
            {
                return await _repo.CreateAsync(profile);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
            return 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var item = await _repo.GetByIdAsync(id);
                if (item != null)
                {
                    return await _repo.RemoveAsync(item);
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
            return false;
        }

        public async Task<List<BearProfile>> GetAllAsync()
        {
            try
            {
                return await _repo.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //return new List<PaymentsAnNtt>();
        }
        public async Task<BearProfile> GetByIdAsync(int id)
        {
            try
            {
                return await _repo.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
            return new BearProfile();
        }

        //public async Task<List<BearProfile>> SearchAsync(decimal paymentAmount, string paymentType, string method)
        //{
        //    try
        //    {
        //        return await _repo.SearchAsync(paymentAmount, paymentType, method);
        //    }
        //    catch (Exception ex)
        //    {
        //        //throw new Exception(ex.Message);
        //    }
        //    return new List<PaymentsAnNtt>();
        //}

        public async Task<PaginationResult<List<BearProfile>>> SearchWithPaginationAsync(BearProfileSearchRequest searchRequest)
        {
            try
            {
                return await _repo.SearchWithPagingAsync(searchRequest);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
            return new PaginationResult<List<BearProfile>>();
        }

        public async Task<int> UpdateAsync(BearProfile profile)
        {
            try
            {
                return await _repo.UpdateAsync(profile);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
            return 0;
        }
    }
}
