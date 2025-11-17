using BearPetManagement_Repository.Models;
using BearPetManagement_Repository.Repositories;

namespace BearPetManagement_Service.Services
{
    public class UserService
    {
        private readonly UserRepository _repo;
        public UserService(UserRepository repo)
        {
            _repo = repo;
        }
        public async Task<BearAccount> GetUser(string userName, string password)
        {
            return await _repo.GetUserByEmailPassword(userName, password);
        }
    }
}
