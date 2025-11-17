using FA25Bear.DataAccess;
using FA25Bear.DataAccess.Models;
using FA25Bear.DataAccess.Models.DTOs;

namespace FA25Bear.Service.Imp
{
    public class BearAccountService : IBearAccountService
    {
        private readonly IBearAccountRepo _repo;
        public BearAccountService(IBearAccountRepo repo)
        {
            _repo = repo;
        }

        public BearAccount CheckLogin(LoginRequestDTO lr)
        {
            string email = lr.Email ?? string.Empty;
            string password = lr.Password ?? string.Empty;
            return _repo.GetSystemAccountByEmaild(email, password);
        }
    }
}
