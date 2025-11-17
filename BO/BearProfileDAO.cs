using BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BearProfileDAO
    {
        private readonly Fa25bearDbContext _context;
        public BearProfileDAO(Fa25bearDbContext context) { 
        _context= context;
        }
        public List<BearProfile> GetAllBears()
        {
            return _context.BearProfiles.Include(b=>b.BearType).ToList();
        }
        public BearProfile? GetBearsById(int id)
        {
            return _context.BearProfiles.FirstOrDefault(b => b.BearProfileId == id);
        }
        public void AddBear(BearProfile bear)
        {
            _context.BearProfiles.Add(bear);
            _context.SaveChanges();
        }
        public void UpdateBear(BearProfile bear)
        {
            _context.BearProfiles.Update(bear);
            _context.SaveChanges();
        }
        public void DeleteBear(int id)
        {
            var bear = _context.BearProfiles.FirstOrDefault(b => b.BearProfileId == id);
            _context.BearProfiles.Remove(bear);
            _context.SaveChanges();
        }
    }
}
