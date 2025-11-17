using Repos;
using Repos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SystemAccountService
    {
        private readonly SystemAccountsRepo _repository;
        public SystemAccountService() => _repository = new SystemAccountsRepo();

        public async Task<BearAccount> GetUserAccount(string email, string password)
        {
            try
            {
                return await _repository.GetUserAccountAsync(email, password);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving the user account: {ex.Message}");
                return null;

            }

        }
    }
}
