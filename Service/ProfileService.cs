using Repo;
using Repo.DTO;
using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProfileService
    {
        private readonly BearProfileRepo _repo;

        public ProfileService() => _repo = new BearProfileRepo();


        public async Task<int> Create(CreateRequest request)
        {
            var dto = new BearProfile
            {
                BearProfileId = 0,
                BearTypeId = request.BearTypeId,
                BearName = request.BearName,
                BearWeight = request.BearWeight,
                Characteristics = request.Characteristics,
                CareNeeds = request.CareNeeds,
                ModifiedDate = request.ModifiedDate
            };

            return await _repo.CreateAsync(dto);
        }
        public async Task<bool> Delete(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null)
            {
                throw new KeyNotFoundException($"BearProfile with ID {id} not found.");
            }
            return await _repo.RemoveAsync(item);
        }

        public async Task<List<BearProfile>> GetAll()
        {
            return await _repo.GetAllInclude();
        }
        public async Task<BearProfile?> GetById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

      
        
    }
}
