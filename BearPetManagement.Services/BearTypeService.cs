using BearPetManagement.Repositories;
using BearPetManagement.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearPetManagement.Services
{
    public class BearTypeService
    {
        private readonly BearTypeRepository _Repository;
        public BearTypeService() => _Repository = new BearTypeRepository();
        public BearTypeService(BearTypeRepository repository) => _Repository = repository;

        public async Task<List<BearType>> GetAllBearTypesAsync()
        {
            return await _Repository.GetAllAsync();
        }
    }
}
