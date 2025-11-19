using Data.IRepository;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PantherRepository : IPantherRepository
    {
        private readonly Su25pantherDbContext _context;

        public PantherRepository(Su25pantherDbContext context)
        {
            _context = context;
        }

        public IQueryable<PantherProfile> GetAll()
        {
            return _context.PantherProfiles.Include(lp => lp.PantherType).OrderByDescending(lp => lp.PantherProfileId);
        }
            
        public PantherProfile GetById(int id)
        {
            return _context.PantherProfiles.Include(lp => lp.PantherType).FirstOrDefault(lp => lp.PantherProfileId == id);
        }

        public void Add(PantherProfile pantherProfile)
        {
            _context.PantherProfiles.Add(pantherProfile);
            _context.SaveChanges();
        }

        public void Update(PantherProfile pantherProfile)
        {
            _context.PantherProfiles.Update(pantherProfile);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var pantherProfile = _context.PantherProfiles.Find(id);
            if (pantherProfile != null)
            {
                _context.PantherProfiles.Remove(pantherProfile);
                _context.SaveChanges();
            }
        }

        public IQueryable<PantherType> GetPantherTypes()
        {
            return _context.PantherTypes;
        }

        public int GetTotalCount()
        {
            return _context.PantherProfiles.Count();
        }
    }
}
