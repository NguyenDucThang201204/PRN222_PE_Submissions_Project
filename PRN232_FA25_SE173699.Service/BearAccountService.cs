using PRN232_FA25_SE173699.Repository.DTOs;
using PRN232_FA25_SE173699.Repository;
using PRN232_FA25_SE173699.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PRN232_FA25_SE173699.Repository.Models;

namespace PRN232_FA25_SE173699.Service
{
    public class BearAccountService 
    {
        private readonly BearAccountRepository _systemAccountRepo;
        public BearAccountService(BearAccountRepository systemAccountRepo)
        {
            _systemAccountRepo = systemAccountRepo;
        }

        public async Task<BearAccount?> LoginAsync(LoginRequest loginDTO)
        {
            var user = await _systemAccountRepo.LoginAsync(loginDTO);

            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}
