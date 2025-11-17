using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class BearProfileRepo : IBearProfileRepo
    {
        private readonly Fa25bearDbContext _context;
        public BearProfileRepo(Fa25bearDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BearProfile>> GetAllAsync()
        {
            return await _context.BearProfiles.Include(h => h.BearType).ToListAsync();
        }
    }
}
