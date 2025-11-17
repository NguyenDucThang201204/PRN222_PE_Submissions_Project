using Microsoft.EntityFrameworkCore;
using pregTrackSys.Repositories.Base;
using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public interface IBearProfilesRepo
    {
        Task<List<BearProfile>> GetBearProfilesAsync();
    }
    public class BearProfilesRepo : GenericRepository<BearProfile>, IBearProfilesRepo
    {
        public BearProfilesRepo() { }

        public async Task<List<BearProfile>> GetBearProfilesAsync()
        {
            return await _context.BearProfile.ToListAsync();
        }
    }
}
