using Repository.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BearTypeRepository : GenericRepository<Models.BearType>

    {
        public BearTypeRepository() { }
        public BearTypeRepository(DBContext.FA25BearDBContext context) => _context = context;
    }
}
