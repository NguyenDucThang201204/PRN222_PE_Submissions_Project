using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BearTypeService
    {
        private readonly BearTypeRepository _repo;
        public BearTypeService() => _repo ??= new BearTypeRepository();

        public async Task<BearType?> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<List<BearType>> GetAll()
        {
            return await _repo.GetAllAsync();
        }
    }
}
