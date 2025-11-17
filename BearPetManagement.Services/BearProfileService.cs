using BearPetManagement.Repositories;
using BearPetManagement.Repositories.ModelExtensions;
using BearPetManagement.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearPetManagement.Services
{
    public class BearProfileService : IBearProfileService
    {
        private readonly BearProfileRepository _Repository;
        public BearProfileService() => _Repository = new BearProfileRepository();
        public BearProfileService(BearProfileRepository repository) => _Repository = repository;
        public async Task<int> CreateAsync(BearProfileDTO bearProfile)
        {
            var data = new BearProfile
            {
                BearProfileId = bearProfile.BearProfileId,
                BearTypeId = bearProfile.BearTypeId,
                BearName = bearProfile.BearName,
                Characteristics = bearProfile.Characteristics,
                BearWeight = bearProfile.BearWeight,
                CareNeeds = bearProfile.CareNeeds,
                ModifiedDate = bearProfile.ModifiedDate
            };
            return await _Repository.CreateAsync(data);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Bear = await _Repository.GetById(id);
            if (Bear != null)
            {
                return await _Repository.RemoveAsync(Bear);
            }
            return false;
        }

        public async Task<List<BearProfile>> GetAllAsync()
        {
            return await _Repository.GetAllAsync();
        }

        public async Task<BearProfile> GetByIdAsync(int id)
        {
            return await _Repository.GetById(id);
        }

        public async Task<int> UpdateAsync(BearProfileDTO bearProfile)
        {
            var data = new BearProfile
            {
                BearProfileId = bearProfile.BearProfileId,
                BearTypeId = bearProfile.BearTypeId,
                BearName = bearProfile.BearName,
                Characteristics = bearProfile.Characteristics,
                BearWeight = bearProfile.BearWeight,
                CareNeeds = bearProfile.CareNeeds,
                ModifiedDate = bearProfile.ModifiedDate
            };
            return await _Repository.UpdateAsync(data);
        }
    }
}
