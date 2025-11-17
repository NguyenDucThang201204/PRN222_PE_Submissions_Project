using Repositories.Dto;
using Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BearService
    {
        private readonly BearProfileRepo _repo;

        public BearService(BearProfileRepo repo)
        {
            _repo = repo;
        }

        public async Task<List<BearProfileDTO>> GetAllHandbags()
        {
            var bears = await _repo.GetBearProfilesAsync();
            return bears.Select(h => new BearProfileDTO
            {
                BearProfileId = h.BearProfileId,
                BearName = h.BearName,
                BearWeight = h.BearWeight,
                Characteristics = h.Characteristics,
                CareNeeds = h.CareNeeds,
                ModifiedDate = h.ModifiedDate,
                BearType = h.BearType.BearTypeName,
            }).ToList();
        }

        public async Task<BearProfileDTO?> GetHandbagById(int id)
        {
            var bear = await _repo.GetBearProfileByIdAsync(id);
            if (bear == null) return null;
            return new BearProfileDTO
            {
                BearProfileId = bear.BearProfileId,
                BearName = bear.BearName,
                BearWeight = bear.BearWeight,
                Characteristics = bear.Characteristics,
                CareNeeds = bear.CareNeeds,
                ModifiedDate = bear.ModifiedDate,
                BearType = bear.BearType.BearTypeName,
            };
        }
    }
}
