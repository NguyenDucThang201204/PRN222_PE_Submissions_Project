using BearPetManagement_Repository.NewFolder;
using BearPetManagement_Repository.Repositories;

namespace BearPetManagement_Service.Services
{
    public class BearService
    {
        private readonly BearRepository _repo;

        public BearService(BearRepository repository)
        {
            _repo = repository;
        }

        public async Task<List<BearProfile>> GetAll()
        {
            return await _repo.GetAllAsync();
        }


    }
}
