using Repositories.Basic;
using Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public interface IAccountService
    {
        Task<BearAccount?> GetAccount(string email, string password);
    }

    public class AccountService : IAccountService
    {
        private readonly GenericRepository<BearAccount> _repository;
        public AccountService(GenericRepository<BearAccount> repository)
        {
            _repository = repository;
        }

        public async Task<BearAccount?> GetAccount(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var userAccount = await _repository
                .GetSet()
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            return userAccount;
        }
    }
}