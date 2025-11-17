using Repositories;
using Repositories.Models;

namespace Services
{
    public class BearTypeService
    {
        private readonly BearTypeRepository _repo;
        public BearTypeService(BearTypeRepository repo) => _repo = repo;
        public async Task<List<BearType>> GetAllAsync() => await _repo.GetAllAsync();
    }
}
