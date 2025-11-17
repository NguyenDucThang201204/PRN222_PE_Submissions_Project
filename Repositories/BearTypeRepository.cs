using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.DBContext;
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
        public BearTypeRepository() => _context ??= new FA25BearDBContext();
        
        public BearTypeRepository(FA25BearDBContext context) => _context = context;
    }
}

