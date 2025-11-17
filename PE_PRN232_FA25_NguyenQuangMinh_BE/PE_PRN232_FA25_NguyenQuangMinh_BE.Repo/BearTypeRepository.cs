using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.Basic;
using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenQuangMinh_BE.Repo
{
    public class BearTypeRepository : GenericRepository<BearTypeRepository>
    {
        public BearTypeRepository() => _context ??= new DBContext.FA25BearDBContext();
        public BearTypeRepository(FA25BearDBContext context) => _context = context;
    }
}
