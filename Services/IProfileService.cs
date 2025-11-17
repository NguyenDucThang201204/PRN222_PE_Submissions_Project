using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.Models;

namespace Services
{
    public interface IProfileService
    {
        Task<List<BearProfile>> GetProfiles();
        Task<BearProfile> GetProfile(int id);
        Task<BearProfile> AddProfile(BearProfile profile);
        Task<BearProfile> UpdateProfile(BearProfile profile);
        Task<BearProfile> DeleteProfile(int id);
        Task<List<BearType>> GetTypes();

        Task<List<BearProfile>> SearchProfile(string keyword);

    }
}
