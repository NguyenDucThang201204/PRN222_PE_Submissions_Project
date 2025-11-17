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
        private readonly BearAccountRepository _repository;

        public BearAccountService() => _repository ??= new BearAccountRepository();

        public async Task<BearAccount?> LogIn(string username, string password)
        {
            return await _repository.LogIn(username, password);
        }
    }
}
