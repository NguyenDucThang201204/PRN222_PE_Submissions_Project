using Repository.Basic;
using Repository.Dbcontext;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BearTypeRepository : GenericRepository<BearType>
    {
        public BearTypeRepository(FA25BearDBContext context) : base(context) { }
    }
}
