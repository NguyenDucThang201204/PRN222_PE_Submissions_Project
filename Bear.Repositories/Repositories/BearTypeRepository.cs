using Bear.Repositories.Basic;
using Bear.Repositories.Interface;
using Bear.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bear.Repositories.Repositories
{
    public class BearTypeRepository : GenericRepository<BearType>, IBearTypeRepository
    {
        public async Task<BearType> GetItemAsyncById(int? id)
        {
            return await _context.BearTypes.FirstOrDefaultAsync(b => b.BearTypeId == id);
        }
    }
}
