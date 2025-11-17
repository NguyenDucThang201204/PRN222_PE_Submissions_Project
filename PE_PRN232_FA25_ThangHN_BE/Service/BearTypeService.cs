using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BearTypeService
    {
        private readonly BearTypeRepository _repo;
        public BearTypeService(BearTypeRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<BearType>> getAllAsync() => await _repo.GetAllAsync();
    }
}
