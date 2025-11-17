using Repo;
using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IBearProfilesService
    {
        Task<List<BearProfile>> GetBearProfilesAsync();
    }
    public class BearProfilesService : IBearProfilesService
    {
        private readonly BearProfilesRepo _repo;

        public BearProfilesService()
        {
            _repo = new BearProfilesRepo();
        }

        public async Task<List<BearProfile>> GetBearProfilesAsync()
        {
            return await _repo.GetBearProfilesAsync();
        }

    }
}
