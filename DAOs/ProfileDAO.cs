using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.Models;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class ProfileDAO
    {
        private static ProfileDAO instance = null;
        //private readonly Fa25bearDbContext context;
        private ProfileDAO()
        {
            //context = new Fa25bearDbContext();
        }
        public static ProfileDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProfileDAO();
                }
                return instance;
            }
        }

        public async Task<List<BearProfile>> GetProfile()
        {
            using (var context = new Fa25bearDbContext())
            {
                var profiles = await context.BearProfiles.Include(p => p.BearType).ToListAsync();
                return profiles;
            }
        }
        public async Task<BearProfile> GetProfileById(int id)
        {
            using (var context = new Fa25bearDbContext())
            {
                var profiles = await context.BearProfiles.Include(p => p.BearType).FirstOrDefaultAsync(profiles => profiles.BearProfileId == id);
                return profiles;
            }
        }
        private string GenerateProfileId()
        {
            var random = new Random();
            var id = random.Next(10000, 99999);
            return "PL" + id.ToString();
        }

        public async Task<BearProfile> AddProfile(BearProfile profile)
        {
            try
            {
                using (var context = new Fa25bearDbContext())
                {


                    await context.BearProfiles.AddAsync(profile);
                    await context.SaveChangesAsync();
                    return profile;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task<BearProfile> UpdateProfile(BearProfile profile)
        {
            using (var context = new Fa25bearDbContext())
            {
                var profileToUpdate = await context.BearProfiles.FirstOrDefaultAsync(p => p.BearProfileId == profile.BearProfileId);
                if (profileToUpdate == null)
                {
                    throw new Exception("Profile not found");
                }
                profileToUpdate.BearTypeId = profile.BearTypeId;
                profileToUpdate.BearName = profile.BearName;
                profileToUpdate.BearWeight = profile.BearWeight;
                profileToUpdate.Characteristics = profile.Characteristics;
                profileToUpdate.CareNeeds = profile.CareNeeds;
                profileToUpdate.ModifiedDate = profile.ModifiedDate;
                context.Update(profileToUpdate);
                await context.SaveChangesAsync();
                return profileToUpdate;
            }
        }

        public async Task<BearProfile> DeleteProfile(int id)
        {
            try
            {
                using (var context = new Fa25bearDbContext())
                {
                    var profileToDelete = await context.BearProfiles.FirstOrDefaultAsync(p => p.BearProfileId.Equals(id));
                    if (profileToDelete == null)
                    {
                        throw new Exception("Profile not found");
                    }
                    context.BearProfiles.Remove(profileToDelete);
                    await context.SaveChangesAsync();

                    return profileToDelete;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BearType>> GetTypes()
        {
            using (var context = new Fa25bearDbContext())
            {
                var types = await context.BearTypes.ToListAsync();
                return types;
            }
        }

        public async Task<List<BearProfile>> SearchProfile(string keyword)
        {
            using (var context = new Fa25bearDbContext())
            {
                keyword = keyword?.Trim().ToLower();

                var query = context.BearProfiles
                    .Include(p => p.BearType)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(keyword))
                {


                    query = query.Where(p =>
                        p.BearName.ToLower().Contains(keyword) ||
                        p.BearWeight.ToString().ToLower().Contains(keyword)

                    ); ;
                }

                return await query.ToListAsync();
            }
        }

    }
}
