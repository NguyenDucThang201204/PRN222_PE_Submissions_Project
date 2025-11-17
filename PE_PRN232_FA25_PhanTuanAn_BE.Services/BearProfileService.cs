using PE_PRN232_FA25_PhanTuanAn_BE.Repositories;
using PE_PRN232_FA25_PhanTuanAn_BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace PE_PRN232_FA25_PhanTuanAn_BE.Services
{
    public class BearProfileService : IBearProfileService
    {
        private readonly BearProfileRepository _repository;
        public BearProfileService() => _repository = new BearProfileRepository();

        public async Task<int> CreateAsync(BearProfile bear)
        {
            try
            {
                return await _repository.CreateAsync(bear);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }

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

            return false;
        }

        public async Task<List<BearProfile>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }

            return new List<BearProfile>();
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

        public async Task<int> UpdateAsync(BearProfile bear)
        {
            try
            {
                return await _repository.UpdateAsync(bear);
            }
            catch (Exception ex) { }

            return 0;
        }
    }
}
