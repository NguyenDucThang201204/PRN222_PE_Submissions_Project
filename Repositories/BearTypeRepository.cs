using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BearTypeRepository
    {
        protected readonly FA25BearDBContext _context;
        public BearTypeRepository()
        {
            _context ??= new();
        }

        public BearTypeRepository(FA25BearDBContext context) => _context = context;

        public async Task<List<BearType>> GetAllAsync() => await _context.BearTypes.ToListAsync();

        public async Task<BearType?> GetByIdAsync(int id)
             => await _context.BearTypes.FirstOrDefaultAsync(i => i.BearTypeId == id);
    }
}
