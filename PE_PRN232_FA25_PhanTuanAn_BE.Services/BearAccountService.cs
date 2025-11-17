using PE_PRN232_FA25_PhanTuanAn_BE.Repositories;
using PE_PRN232_FA25_PhanTuanAn_BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_PhanTuanAn_BE.Services
{
    public class BearAccountService
    {
        private readonly BearAccountRepository _repository;
        public BearAccountService() => _repository = new BearAccountRepository();
        public async Task<BearAccount> GetUserAccount(string email, string password)
        {
            return await _repository.GetUserAccount(email, password);
        }
    }
}
