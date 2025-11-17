using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class BearProfileRepo : GenericRepository<BearProfile>
    {
        public BearProfileRepo() : base() { }

        public async Task<BearProfile?> GetById(int id)
        {
            return await _context.BearProfiles.Include(b => b.BearType).FirstOrDefaultAsync();
        }

        public async Task<List<BearProfile>> GetAll()
        {
            return await _context.BearProfiles.Include(b => b.BearType).ToListAsync();
        }
    }
}
