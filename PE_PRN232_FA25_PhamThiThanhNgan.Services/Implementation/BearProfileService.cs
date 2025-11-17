using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Interface;
using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.ModelExtensions;
using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Models;
using Practice_FA25_PE_Services.DTO;
using Practice_FA25_PE_Services.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Practice_FA25_PE_Services.Implementation
{
    public class BearProfileService : IBearProfileService
    {
        private readonly IBearProfileRepository _profileRepository;
        private readonly IBearTypeRepository _typeRepository;

        public BearProfileService(IBearProfileRepository profileRepository, IBearTypeRepository typeRepository)
        {
            _profileRepository = profileRepository;
            _typeRepository = typeRepository;
        }

        public async Task<IEnumerable<BearProfileDTO>> GetAllProfilesAsync()
        {
            var profiles = await _profileRepository.GetAllAsync();

            return profiles.Select(p => new BearProfileDTO
            {
                BearProfileId = p.BearProfileId,
                BearTypeId = p.BearTypeId,
                BearName = p.BearName,
                Weight = p.Weight,
                ModifiedDate = p.ModifiedDate,
                CareNeeds = p.CareNeeds,
                Characteristics = p.Characteristics,
                BearTypeName = p.BearType?.BearTypeName ?? "Unknown"
            }).ToList();
        }

        public async Task<BearProfileDTO?> GetProfileByIdAsync(int id)
        {
            var profile = await _profileRepository.GetByIdAsync(id);

            if (profile == null)
            {
                return null;
            }

            return new BearProfileDTO
            {
                BearProfileId = profile.BearProfileId,
                BearTypeId = profile.BearTypeId,
                BearName = profile.BearName,
                Weight = profile.Weight,
                CareNeeds = profile.CareNeeds,
                Characteristics = profile.Characteristics,
                ModifiedDate = profile.ModifiedDate,
                BearTypeName = profile.BearType?.BearTypeName ?? "Unknown"
            };
        }

        public IQueryable<BearProfileDTO> GetQueryableProfiles()
        {
            var query = _profileRepository.GetQueryable();
            return query.Select(p => new BearProfileDTO
            {
                BearProfileId = p.BearProfileId,
                BearTypeId = p.BearTypeId,
                BearName = p.BearName,
                Weight = p.Weight,
                CareNeeds = p.CareNeeds,
                Characteristics = p.Characteristics,
                ModifiedDate = p.ModifiedDate,
                BearTypeName = p.BearType.BearTypeName
            });
        }

        public async Task<BearProfileDTO> CreateProfileAsync(BearProfileRequestDTO profileDto)
        {
            var typeExists = await _typeRepository.GetByIdAsync(profileDto.BearTypeId);
            if (typeExists == null)
            {
                throw new KeyNotFoundException($"BearTypeId {profileDto.BearTypeId} does not exist.");
            }

            var profile = new BearProfile
            {
                BearTypeId = profileDto.BearTypeId,
                BearName = profileDto.BearName,
                Weight = profileDto.Weight,
                Characteristics = profileDto.Characteristics,
                CareNeeds = profileDto.CareNeeds,
                ModifiedDate = DateTime.Now
            };

            await _profileRepository.AddAsync(profile);
            await _profileRepository.SaveAsync();

            var createdProfile = await _profileRepository.GetByIdAsync(profile.BearProfileId)
                ?? throw new InvalidOperationException("Profile created but cannot be retrieved.");

            return new BearProfileDTO
            {
                BearProfileId = createdProfile.BearProfileId,
                BearTypeId = createdProfile.BearTypeId,
                BearName = createdProfile.BearName,
                Weight = createdProfile.Weight,
                Characteristics = createdProfile.Characteristics,
                CareNeeds = createdProfile.CareNeeds,
                ModifiedDate = createdProfile.ModifiedDate,
                BearTypeName = createdProfile.BearType?.BearTypeName ?? "Unknown"
            };
        }

        public async Task<BearProfileDTO?> UpdateProfileAsync(int id, BearProfileRequestDTO profileDto)
        {
            var profile = await _profileRepository.GetByIdAsync(id);
            if (profile == null)
            {
                return null;
            }
            var typeExists = await _typeRepository.GetByIdAsync(profileDto.BearTypeId);
            if (typeExists == null)
            {
                throw new KeyNotFoundException($"BearTypeId {profileDto.BearTypeId} does not exist.");
            }

            profile.BearTypeId = profileDto.BearTypeId;
            profile.BearName = profileDto.BearName;
            profile.Weight = profileDto.Weight;
            profile.Characteristics = profileDto.Characteristics;
            profile.CareNeeds = profileDto.CareNeeds;
            profile.ModifiedDate = DateTime.UtcNow; 

            _profileRepository.Update(profile);
            await _profileRepository.SaveAsync();

            var updatedProfile = await _profileRepository.GetByIdAsync(profile.BearProfileId)
                ?? throw new InvalidOperationException("Profile updated but cannot be retrieved.");

            return new BearProfileDTO
            {
                BearProfileId = updatedProfile.BearProfileId,
                BearTypeId = updatedProfile.BearTypeId,
                BearName = updatedProfile.BearName,
                Weight = updatedProfile.Weight,
                Characteristics = updatedProfile.Characteristics,
                CareNeeds = updatedProfile.CareNeeds,
                ModifiedDate = updatedProfile.ModifiedDate,
                BearTypeName = updatedProfile.BearType?.BearTypeName ?? "Unknown"
            };
        }

        public async Task<bool> DeleteProfileAsync(int id)
        {
            var profile = await _profileRepository.GetByIdAsync(id);
            if (profile == null)
            {
                return false;
            }

            _profileRepository.Delete(profile);
            await _profileRepository.SaveAsync();
            return true;
        }

        public async Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(BearSearchRequest searchrequest)
        {
            try
            {
                return await _profileRepository.SearchWithPagingAsync(searchrequest);
            }
            catch (Exception ex) { }
            return new PaginationResult<List<BearProfile>>();
        }
    }
}
