using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo;
using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenQuangMinh_BE.Service
{
    public class SystemUserAccountService
    {
        private readonly SystemUserAccountRepository _repository;
        public SystemUserAccountService() => _repository = new SystemUserAccountRepository();

        public async Task<BearAccount> GetUserAccount(string username, string password)
        {
            try
            {
                return await _repository.GetUserAccountAsync(username, password);
            }
            catch (Exception ex)
            {


            }
            return null;

        }
    }
}
