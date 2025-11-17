using FA25Bear.Repositories.DTOs;
using FA25Bear.Repositories.IRepositories;
using FA25Bear.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA25Bear.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public AuthResponse Authenticate(string email, string password)
        {
            return _accountRepository.Authenticate(email, password);
        }
    }
}
