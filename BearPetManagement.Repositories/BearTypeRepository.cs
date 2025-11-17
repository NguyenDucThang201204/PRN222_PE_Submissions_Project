using BearPetManagement.Repositories.Basic;
using BearPetManagement.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearPetManagement.Repositories
{
    public class BearTypeRepository : GenericRepository<BearType>
    {
        public BearTypeRepository() { }
        public BearTypeRepository(FA25BearDBContext context) => _context = context;
    }
}
