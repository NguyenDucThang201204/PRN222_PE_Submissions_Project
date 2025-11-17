using Microsoft.EntityFrameworkCore;
using PRN232_FA25_SE173699.Repository.DTOs;
using PRN232_FA25_SE173699.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_FA25_SE173699.Repository
{
    public class BearAccountRepository
    {
        private readonly FA2025BearDBContext _context;

        public BearAccountRepository(FA2025BearDBContext context)
        {
            _context = context;
        }
        public async Task<BearAccount?> LoginAsync(LoginRequest loginDTO)
        {
            var user = await _context.BearAccounts
                .FirstOrDefaultAsync(acc => acc.Email == loginDTO.Email && acc.Password == loginDTO.Password);

            return user;
        }
    }
}
