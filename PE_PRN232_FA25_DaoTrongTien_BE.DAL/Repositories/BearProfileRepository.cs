using PE_PRN232_FA25_DaoTrongTien_BE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_DaoTrongTien_BE.DAL.Repositories
{
    public class BearProfileRepository:IBearProfileRepository
    {

        private readonly Fa25bearDbContext _context;
        public BearProfileRepository(Fa25bearDbContext context)
        {
            _context = context;
        }

        public async Task<List<BearProfile>> GetAllBearProfiles()
        {
            return _context.BearProfiles.ToList();
        }

        public async Task<BearProfile> GetBearProfileById(int id)
        {
            return _context.BearProfiles.FirstOrDefault(h => h.BearProfileId == id);
        }

        public async Task AddBearProfile(BearProfile BearProfile)
        {
            _context.BearProfiles.Add(BearProfile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBearProfile(BearProfile BearProfile)
        {
            _context.Entry(BearProfile).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBearProfile(int id)
        {
            var BearProfile = _context.BearProfiles.FirstOrDefault(h => h.BearProfileId == id);
            if (BearProfile != null)
            {
                _context.BearProfiles.Remove(BearProfile);
                await _context.SaveChangesAsync();
            }
        }

    }
}
