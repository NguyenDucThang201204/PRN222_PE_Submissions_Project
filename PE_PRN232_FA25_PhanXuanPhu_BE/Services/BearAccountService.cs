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
        private readonly BearAccountRepository _repo;

        public BearAccountService()
        {
            _repo = new BearAccountRepository();
        }

        public async Task<BearAccount> LoginAsync(string email, string password)
        {
            return await _repo.LoginAsync(email, password);
        }
    }
}
