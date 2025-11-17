using PE__PRN232_FA25_NamPK_BLL.DTO;
using PE__PRN232_FA25_NamPK_DAL;
using PE__PRN232_FA25_NamPK_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE__PRN232_FA25_NamPK_BLL
{
    public class BearProfileService
    {
        private readonly IGenericRepository<BearProfile> _bearProfileRepository;

        public BearProfileService(IGenericRepository<BearProfile> bearProfileRepository)
        {
            _bearProfileRepository = bearProfileRepository;
        }

        public async Task<IEnumerable<BearProfile>> GetAllAsync()
        {
            var bearProfiles = await _bearProfileRepository.GetAllAsync(h => h.BearType);
            return bearProfiles;
        }

        public async Task<BearProfile> GetByIdAsync(int id) => await _bearProfileRepository.GetByIdAsync(id, h => h.BearType);

        public async Task CreateHandBagAsync(BearProfileRequest bearProfile)
        {
            var newBearProfile = new BearProfile
            {
                BearTypeId = bearProfile.BearTypeId,

                BearName = bearProfile.BearName,

                BearWeight = bearProfile.BearWeight,

                Characteristics = bearProfile.Characteristics,

                CareNeeds = bearProfile.CareNeeds,

                ModifiedDate = bearProfile.ModifiedDate
            };

            await _bearProfileRepository.AddAsync(newBearProfile);
            await _bearProfileRepository.SaveChangesAsync();
        }

        public async Task UpdateHandBagAsync(int id, BearProfileRequest bearProfile)
        {
            var existBear = await GetByIdAsync(id);

            if (bearProfile.BearTypeId > 0)
            {
                existBear.BearTypeId = bearProfile.BearTypeId;
            }

            if (!string.IsNullOrEmpty(bearProfile.BearName))
            {
                existBear.BearName = bearProfile.BearName;
            }
            if (!string.IsNullOrEmpty(bearProfile.Characteristics))
            {
                existBear.Characteristics = bearProfile.Characteristics;
            }

            if (bearProfile.BearWeight.HasValue && bearProfile.BearWeight > 0)
            {
                existBear.BearWeight = bearProfile.BearWeight;
            }

            if (bearProfile.CareNeeds.HasValue && bearProfile.CareNeeds >= 0)
            {
                existBear.CareNeeds = bearProfile.CareNeeds;
            }

            if (bearProfile.ModifiedDate.HasValue)
            {
                existBear.ModifiedDate = bearProfile.ModifiedDate;
            }
            _bearProfileRepository.Update(existBear);
            await _bearProfileRepository.SaveChangesAsync();
        }

        public async Task DeleteHandBagAsync(BearProfile bearProfile)
        {
            _bearProfileRepository.Remove(bearProfile);
            await _bearProfileRepository.SaveChangesAsync();
        }

    }
}
