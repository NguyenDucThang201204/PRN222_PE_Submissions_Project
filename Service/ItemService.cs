using Repository.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ItemService : IItemService
    {
        private readonly IGenericRepository<BearProfile> _repo;

        public ItemService(IGenericRepository<BearProfile> repo)
        {
            _repo = repo;
        }

        public async Task<List<BearProfile>> GetAll()
        {
            var result = await _repo.GetAllAsync(null, r => r.BearType);
            return result.ToList();
        }

        public async Task<BearProfile> GetByIdAsync(int id)
        {
            var result = await _repo.GetSingleByConditionAsynce(i => i.BearProfileId == id, r => r.BearType);

            return result;
        }

        public async Task<BearProfile> AddAsync(BearProfile item)
        {
            try
            {
                if (item != null)
                {
                    await _repo.AddAsync(item);
                    return item;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception($"Could not add {ex.Message}");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {

                var result = await _repo.GetByIdAsync(id);
                if (result != null)
                {
                    return await _repo.DeleteAsync(result);
                }
                return false;

            }
            catch (Exception ex)
            {
                throw new Exception($"Could not delete {ex.Message}");
            }
        }

        public async Task<BearProfile> UpdateAsynce(BearProfile item)
        {
            try
            {
                await _repo.UpdateAsync(item);
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update {ex.Message}");
            }
        }

        public async Task<List<BearProfile>> Search(string bearName, double? weight, string? bearTypeName)
        {
            Expression<Func<BearProfile, bool>> filter = null;

            if (!string.IsNullOrEmpty(bearName) && weight.HasValue && !string.IsNullOrEmpty(bearTypeName))
            {
                filter = i => i.BearName.Contains(bearName) && i.Weight.Equals(weight.Value) && i.BearType.BearTypeName.Contains(bearTypeName);
            }
            else if (!string.IsNullOrEmpty(bearName))
            {
                filter = i => i.BearName.Contains(bearName);
            }
            else if (weight.HasValue)
            {
                filter = i => i.Weight.Equals(weight.Value);
            }
            else if (!string.IsNullOrEmpty(bearTypeName))
            {
                filter = i => i.BearType.BearTypeName.Contains(bearTypeName);
            }

            var result = await _repo.GetAllAsync(filter, r => r.BearType);
            return result.ToList();
        }
    }
}
