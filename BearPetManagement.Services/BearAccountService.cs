using BearPetManagement.Repositories;
using BearPetManagement.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearPetManagement.Services
{
    public class BearAccountService
    {
        private readonly BearAcountRepository _AccountRepository;
        public BearAccountService() => _AccountRepository ??= new BearAcountRepository();

        public async Task<BearAccount?> GetUserAccount(string email, string password)
        {
            return await _AccountRepository.GetUserAccount(email, password);
        }
    }
}
