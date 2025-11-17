using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.Models;

namespace Repositories
{
    public class BearTypeRepository : GenericRepository<BearType>
    {
        public BearTypeRepository(BearManagementDbContext context) : base(context)
        {
        }
    }
}
