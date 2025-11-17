using Repo;
using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BearAccountService
    {
        private readonly BearAccountRepo _repo;
        public BearAccountService() => _repo = new BearAccountRepo();

        public async Task<BearAccount> GetSystemUserAccountAsync(string username, string password)
        {
            return await _repo.GetUserAccount(username, password);
        }
    }
}
