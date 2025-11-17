using Microsoft.EntityFrameworkCore;
using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class BearProfileRepo :GenericRepository<BearProfile>
    {
        public BearProfileRepo()
        {

        }
        public new async Task<List<BearProfile>> GetAllInclude()
        {
            var itemList = await _context.BearProfiles
                .Include(x => x.BearType)
                .ToListAsync();

            return itemList;

        }
        public new async Task<BearProfile> GetByIdAsync(int id)
        {
            return await _context.BearProfiles.Include(x => x.BearType).
                FirstOrDefaultAsync(x => x.BearProfileId == id);
        }
    }
}
