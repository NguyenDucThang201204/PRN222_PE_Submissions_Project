using Microsoft.EntityFrameworkCore;
using Repos.Basic;
using Repos.DBContext;
using Repos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public class SubRepo : GenericRepository<BearType>
    {
        public SubRepo() => _context ??= new DBContext.FA2025BearDBContext();
        public SubRepo(FA2025BearDBContext context) => _context = context;

    }
}
