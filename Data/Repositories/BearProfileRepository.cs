using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class BearProfileRepository : IBearProfileRepository
    {
        private readonly FA25BearDBContext _context;
        public BearProfileRepository(FA25BearDBContext context) => _context = context;

        public IEnumerable<BearProfile> GetAll() => _context.BearProfiles.ToList();
        public BearProfile? GetById(int id) => _context.BearProfiles.Find(id);
        public void Add(BearProfile bear) => _context.BearProfiles.Add(bear);
        public void Update(BearProfile bear) => _context.BearProfiles.Update(bear);
        public void Delete(BearProfile bear) => _context.BearProfiles.Remove(bear);
        public void Save() => _context.SaveChanges();
    }
}
