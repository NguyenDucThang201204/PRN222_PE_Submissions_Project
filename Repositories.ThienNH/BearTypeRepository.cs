using Microsoft.EntityFrameworkCore;
using Repositories.ThienNH.Basic;
using Repositories.ThienNH.DBContext;
using Repositories.ThienNH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ThienNH
{
    public class BearTypeRepository : GenericRepository<BearType>
    {
        public BearTypeRepository() { }
        public BearTypeRepository(FA25BearDBContext context) => _context = context;

        public async Task<List<BearType>> GetAllAsync()
        {
            return await _context.BearTypes.ToListAsync();
        }

        public async Task<BearType> GetByIdAsync(int id)
        {
            var item = await _context.BearTypes.FirstOrDefaultAsync(s => s.BearTypeId == id);
            return item ?? new BearType();
        }
    }
}
