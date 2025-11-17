using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BearTypeRepository : GenericRepository<BearType>
    {
        private readonly FA25BearDBContext _context;

        public BearTypeRepository() => _context ??= new FA25BearDBContext();

        public async Task<BearType?> GetById(int id)
        {
            return await _context.BearTypes.FirstOrDefaultAsync(x => x.BearTypeId == id);
        }
    }
}
