using BusinessObjects;
using BusinessObjects.DTOs;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepo _profileRepo;

        public ProfileService(IProfileRepo profileRepo)
        {
            _profileRepo = profileRepo;
        }
        public async Task<GetDTO?> CreateAsync(CreateDTO profileDTO)
        {
            var b = await _profileRepo.GetTypeById(profileDTO.BearTypeId);
            if (b == null)
            {
                return null;
            }

            BearProfile hb = new()
            {
                BearName = profileDTO.BearName,
                BearWeight = profileDTO.Weight,
                Characteristics = profileDTO.Characteristics,
                CareNeeds = profileDTO.CareNeeds,
                BearTypeId = profileDTO.BearTypeId,
                ModifiedDate = profileDTO.ModifiedDate
            };

            return await _profileRepo.CreateAsync(hb);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _profileRepo.DeleteAsync(id);
        }

        public async Task<List<GetDTO>> GetAllAsync()
        {
            return await _profileRepo.GetAllAsync();
        }

        public async Task<GetDTO?> GetByIdAsync(int id)
        {
            return await _profileRepo.GetById(id);
        }

        public async Task<GetDTO?> UpdateAsync(int id, CreateDTO request)
        {
            BearProfile old = (await _profileRepo.GetByIdForUpdate(id))!;
            var b = await _profileRepo.GetTypeById(request.BearTypeId);
            if (b == null)
            {
                return null;
            }

            old.BearName = request.BearName;
            old.BearWeight = request.Weight;
            old.Characteristics = request.Characteristics;
            old.CareNeeds = request.CareNeeds;
            old.BearTypeId = request.BearTypeId;
            old.ModifiedDate = request.ModifiedDate;

            return await _profileRepo.UpdateAsync(old);
        }
    }
}
