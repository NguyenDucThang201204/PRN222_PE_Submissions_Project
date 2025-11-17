using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BearAccountService
    {
        private readonly BearAccountRepo _repo;
        public BearAccountService() => _repo = new BearAccountRepo();
        public async Task<BearAccount> GetAccount(string email, string password)
        {
            try
            {
                return await _repo.GetUserAccount(email, password);
            }
            catch 
            { 
            }
            return null;
        }
    }
}
