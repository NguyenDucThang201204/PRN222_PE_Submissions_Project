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
        private readonly BearTypeRepository _repository;

        public BearTypeService(BearTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<BearType> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
