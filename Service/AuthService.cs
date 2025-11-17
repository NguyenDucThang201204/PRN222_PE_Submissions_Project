using Repo;
using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IAuthService
    {
        Task<BearAccount> Authenticate(string email, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly AuthRepo _repo;

        public AuthService()
        {
            _repo = new AuthRepo();
        }

        public async Task<BearAccount> Authenticate(string email, string password)
        {
            return await _repo.GetAccount(email, password);
        }
    }
}
