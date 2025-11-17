using PE_PRN232_FA25_DaoTrongTien_BE.DAL.Models;
using PE_PRN232_FA25_DaoTrongTien_BE.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_DaoTrongTien_BE.BLL.Services
{
    public class BearProfileService:IBearProfileService
    {

        private readonly IBearProfileRepository _repo;
        public BearProfileService(IBearProfileRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<BearProfile>> GetAllBearProfiles()
        {
            return await _repo.GetAllBearProfiles();
        }

        public async Task<BearProfile> GetBearProfileById(int id)
        {
            return await _repo.GetBearProfileById(id);
        }
        public async Task AddBearProfile(BearProfile BearProfile)
        {
            await _repo.AddBearProfile(BearProfile);
        }

        public async Task UpdateBearProfile(BearProfile BearProfile)
        {
            await _repo.UpdateBearProfile(BearProfile);
        }

        public async Task DeleteBearProfile(int id)
        {
            await _repo.DeleteBearProfile(id);
        }

    }
}
