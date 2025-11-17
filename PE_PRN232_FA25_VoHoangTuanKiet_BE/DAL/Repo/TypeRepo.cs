using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class TypeRepo
    {
        private readonly Fa25bearDbContext _context;

        public TypeRepo(Fa25bearDbContext context)
        {
            _context = context;
        }

        public BearType? GetById(int id)
        {
            return _context.BearTypes.Find(id);
        }

        public List<BearType> GetAll()
        {
            return _context.BearTypes.ToList();
        }
    }
}
