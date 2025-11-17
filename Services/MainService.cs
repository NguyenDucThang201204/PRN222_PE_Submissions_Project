using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MainService
    {
        private readonly MainRepository _repository;

        public MainService()
        {
            _repository ??= new MainRepository();
        }
        public async Task<List<BearProfile>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving items: {ex.Message}");
            }
        }

        public Task<BearProfile> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }
    }
}
