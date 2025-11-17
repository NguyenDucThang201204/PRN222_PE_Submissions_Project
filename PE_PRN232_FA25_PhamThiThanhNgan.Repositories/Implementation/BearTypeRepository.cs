using Microsoft.EntityFrameworkCore;
using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Interface;
using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Models;



namespace PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Implementation
{
    public class BearTypeRepository : IBearTypeRepository
    {
        private readonly Fa25bearDbContext _context;

        public BearTypeRepository(Fa25bearDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BearType>> GetAllAsync()
        {
            return await _context.BearTypes.ToListAsync();
        }
        public async Task<BearType?> GetByIdAsync(int id)
        {
            return await _context.BearTypes.FindAsync(id);
        }
    }
}
