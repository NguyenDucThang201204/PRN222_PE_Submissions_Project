using BearPetManagement_Repositories.DBContext;
using BearPetManagement_Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearPetManagement_Repositories
{
    public class BearProfileRepository : GenericRepository<BearProfile>
    {
        public BearProfileRepository() : base() { }
        public BearProfileRepository(FA25BearDBContext context) : base(context) { }


    }
}
