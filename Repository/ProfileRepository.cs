using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.Models;
using DAOs;

namespace Repository
{
    public class ProfileRepository : IProfileRepository
    {
        public async Task<BearProfile> AddProfile(BearProfile profile)
        {
            return await ProfileDAO.Instance.AddProfile(profile);
        }

        public async Task<BearProfile> DeleteProfile(int id)
        {
            return await ProfileDAO.Instance.DeleteProfile(id);
        }

        public async Task<BearProfile> GetProfile(int id)
        {
            return await ProfileDAO.Instance.GetProfileById(id);
        }

        public async Task<List<BearProfile>> GetProfiles()
        {
            return await ProfileDAO.Instance.GetProfile();
        }

        public async Task<List<BearType>> GetTypes()
        {
            return await ProfileDAO.Instance.GetTypes();
        }

        public async Task<BearProfile> UpdateProfile(BearProfile profile)
        {
            return await ProfileDAO.Instance.UpdateProfile(profile);
        }
        public async Task<List<BearProfile>> SearchProfile(string keyword)
        {
            return await ProfileDAO.Instance.SearchProfile(keyword);
        }
    }
}
