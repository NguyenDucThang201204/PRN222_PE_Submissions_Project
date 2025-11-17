using BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class BearProfileDAO
    {
        private readonly Fa25bearDbContext _context;

        public BearProfileDAO (Fa25bearDbContext context)
        {
            _context = context;
        }

        public BearProfile GetBearProfileById(int id)
        {
            return _context.BearProfiles.Include(b => b.BearType).SingleOrDefault(m => m.BearProfileId == id);
        }

        public List<BearProfile> GetBearList()
        {
            return _context.BearProfiles.Include(h => h.BearType).ToList();
        }

        public BearProfile CreateBear(BearProfile bear)
        {
            
            if (!_context.BearTypes.Any(b => b.BearTypeId == bear.BearTypeId))
                throw new Exception("Invalid BrandId");

            _context.BearProfiles.Add(bear);
            _context.SaveChanges();
            return bear;

        }

        public void UpdateBear(BearProfile bear)
        {

            _context.BearProfiles.Update(bear);
            _context.SaveChanges();

        }

        public void DeleteBear(int id)
        {
            var bearRemove = _context.BearProfiles.SingleOrDefault(h => h.BearProfileId == id);
            _context.BearProfiles.Remove(bearRemove);
            _context.SaveChanges();

        }

       
    }
}
