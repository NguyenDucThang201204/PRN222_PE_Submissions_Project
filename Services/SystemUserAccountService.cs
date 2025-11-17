using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SystemUserAccountService
    {
        private readonly SystemUserAccountRepository _repository;

        public SystemUserAccountService() => _repository ??= new SystemUserAccountRepository();

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
