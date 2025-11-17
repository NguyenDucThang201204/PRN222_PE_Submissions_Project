using Repository.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<BearAccount> _repository;

        public AuthService(IGenericRepository<BearAccount> repository)
        {
            _repository = repository;
        }

        public async Task<BearAccount> Login(string userName, string password)
        {
            var account = await _repository.GetSingleByConditionAsynce(u => u.Email == userName & u.Password == password);
            return account;
        }
    }
}
