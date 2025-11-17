using Repository.Models;
using Repository.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BearProfileService
    {
        private BearProfileRepo bearProfileRepo;

        public BearProfileService(BearProfileRepo bearProfileRepo)
        {
            this.bearProfileRepo = bearProfileRepo;
        }

        public async Task<BearProfile?> GetById(int id)
        {
            return await bearProfileRepo.GetById(id);
        }

        public async Task<List<BearProfile>> GetAll()
        {
            return await bearProfileRepo.GetAll();
        }

        public async Task<int> CreateBear(BearProfile bearProfile)
        {
            return await bearProfileRepo.CreateAsync(bearProfile);
        }

        public async Task<int> UpdateBear(BearProfile bearProfile)
        {
            bearProfile.BearType = null;
            return await bearProfileRepo.UpdateAsync(bearProfile);
        }

        public async Task<bool> DeleteBear(BearProfile bearProfile)
        {
            return await bearProfileRepo.RemoveAsync(bearProfile);
        }
    }
}
