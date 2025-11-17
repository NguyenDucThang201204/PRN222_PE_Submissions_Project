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
        private readonly BearTypeRepository _repository;

        public BearTypeService()
        {
            _repository = new BearTypeRepository();
        }

        public BearTypeService(BearTypeRepository repo)
        =>
            _repository = repo;

        public Task<List<BearType>> GetAllAsync()
        {
            var response = _repository.GetAllAsync();
            return response;
        }

        public Task<BearType> GetByIdAsync(int id)
        {
            var response = _repository.GetByIdAsync(id);
            return response;
        }
    }
}
