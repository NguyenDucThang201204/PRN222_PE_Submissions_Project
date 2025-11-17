using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositores;
using Repositores.Models;

namespace Services
{
    public class BearAccountService
    {
        private readonly BearAccountRepository _repository;
        public BearAccountService(BearAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<BearAccount> GetUserAccount(string email, string password)
        {
            try
            {
                return await _repository.GetUserAccount(email, password);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
