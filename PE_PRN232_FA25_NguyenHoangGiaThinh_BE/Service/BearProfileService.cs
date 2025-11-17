using BO;
using BO.Dto;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service
{
    public class BearProfileService : IBearProfileService
    {
        private readonly IBearProfileRepository _repository;
        public BearProfileService(IBearProfileRepository repository) 
        {
            _repository = repository;
        }

        public BearProfile CreateBear(BearDto b)
        {
            Validate(b);
            var bear = new BearProfile
            {
                BearName = b.BearName.Trim(),
                BeartWeight = b.BeartWeight,
                BearTypeId = b.BearTypeId,
                Characteristics = b.Characteristics,
                CareNeeds = b.CareNeeds,
                ModifiedDate = DateTime.UtcNow
            };

            _repository.CreateBear(bear);
            return bear;
        }

        public void DeleteBear(int id)
        {
            _repository.DeleteBear(id);
        }

        public List<BearProfile> GetBearList()
        {
            return _repository.GetBearList();
        }

        public BearProfile GetBearProfileById(int id)
        {
           return  _repository.GetBearProfileById(id);
        }

        public void UpdateBear(int id,BearDto bear)
        {
            Validate(bear);
            var existing = _repository.GetBearProfileById(id);
            if (existing == null)
                throw new Exception($"Bear with ID {id} not found.");
            var b = new BearProfile
            {
                BearName = bear.BearName.Trim(),
                BeartWeight = bear.BeartWeight,
                BearTypeId = bear.BearTypeId,
                Characteristics = bear.Characteristics,
                CareNeeds = bear.CareNeeds,
                ModifiedDate = DateTime.UtcNow
            };

            _repository.UpdateBear(b);
            
        }

        public void Validate(BearDto h)
        {
            string pattern = @"^([A-Za-z0-9][A-Za-z0-9\s]*([A-Za-z0-9][A-Za-z0-9]*))$";
            if (string.IsNullOrEmpty(h.BearName) || !Regex.IsMatch(h.BearName, pattern))
            {
                throw new ArgumentException("name is invalid");
            }

            if (h.BeartWeight <= 200)
            {
                throw new ArgumentException("Weitght must be greater than 200");
            }
        }
    }
}
