using Repositories.ThienNH;
using Repositories.ThienNH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ThienNH
{
    public class BearTypeService : IBearTypeService
    {
        private readonly BearTypeRepository _repository;
        public BearTypeService() => _repository = new BearTypeRepository();


        public async Task<List<BearType>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {

            }
            return new List<BearType>();
        }

        public async Task<BearType> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex) { }
            return new BearType();
        }
    }
}
