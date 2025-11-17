using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ModelService
    {
        private readonly ModelRepo _modelservice;

        public ModelService(ModelRepo modelRepo)
        {
            _modelservice = modelRepo;
        }

        public async Task<List<BearProfile>> GetAllAsync()
        {
            return await _modelservice.GetAllAsync();
        }

        public async Task<BearProfile> GetByIdAsync(int modelId)
        {
            return await _modelservice.GetByIdAsync(modelId);
        }

        public async Task<bool> CreateAsync(BearProfile entity)
        {

            return await _modelservice.CreateAsync(entity);
        }

        public async Task<bool> UpdateAsync(BearProfile entity)
        {
            return await _modelservice.UpdateAsync(entity);
        }
        public async Task<bool> DeletetAsync(int id)
        {
            return await _modelservice.DeletetAsync(id);
        }
    }
}
