using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.Models;
using Repository;

namespace Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository profileRepository;
        public ProfileService(IProfileRepository profileRepo)
        {
            profileRepository = profileRepo;
        }

        public async Task<BearProfile> AddProfile(BearProfile profile)
        {

            return await profileRepository.AddProfile(profile);
        }

        public async Task<BearProfile> DeleteProfile(int id)
        {
            return await profileRepository.DeleteProfile(id);
        }

        public async Task<BearProfile> GetProfile(int id)
        {
            return await profileRepository.GetProfile(id);
        }

        public async Task<List<BearProfile>> GetProfiles()
        {
            return await profileRepository.GetProfiles();
        }

        public async Task<List<BearType>> GetTypes()
        {
            return await profileRepository.GetTypes();
        }

        public async Task<BearProfile> UpdateProfile(BearProfile profile)
        {
            return await profileRepository.UpdateProfile(profile);
        }

        public async Task<List<BearProfile>> SearchProfile(string keyword)
        {
            return await profileRepository.SearchProfile(keyword);
        }
    }
}
