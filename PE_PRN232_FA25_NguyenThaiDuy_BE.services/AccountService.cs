using PE_PRN232_FA25_NguyenThaiDuy_BE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenThaiDuy_BE.services
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
