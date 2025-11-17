using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class ProfileRepo
    {
        private readonly Fa25bearDbContext _context;

        public ProfileRepo(Fa25bearDbContext context)
        {
            _context = context;
        }

        public BearProfile? GetById(int id)
        {
            return _context.BearProfiles.Where(i => i.BearProfileId == id)
                .Include(i => i.BearType)
                .FirstOrDefault();
        }

        public List<BearProfile> GetAll()
        {
            return _context.BearProfiles.Where(i => true)
                .Reverse()
                .Include(i => i.BearType)
                .ToList();
        }

        public bool Add(BearProfile BearProfile)
        {
            _context.BearProfiles.Add(BearProfile);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var BearProfile = _context.BearProfiles.Find(id);
            if (BearProfile == null) return false;
            _context.BearProfiles.Remove(BearProfile);
            return _context.SaveChanges() > 0;
        }

        public bool Update(BearProfile BearProfile)
        {
            _context.BearProfiles.Update(BearProfile);
            return _context.SaveChanges() > 0;
        }
    }
}
