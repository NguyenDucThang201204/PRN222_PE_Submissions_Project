using Bussiness.Interfaces;
using Data.IRepository;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Services
{
    public class PantherService : IPantherService
    {
        private readonly IPantherRepository _repository;

        public PantherService(IPantherRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<PantherViewModel> GetPantherProfiles(int pageNumber, int pageSize)
        {
            return _repository.GetAll()
                .Select(lp => new PantherViewModel
                {
                    Id = lp.PantherProfileId,
                    PantherName = lp.PantherName,
                    Weight = lp.Weight,
                    PantherTypeName = lp.PantherType.PantherTypeName
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public PantherProfile GetPantherProfileById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddPantherProfile(PantherProfile pantherProfile)
        {
            _repository.Add(pantherProfile);
        }

        public void UpdatePantherProfile(PantherProfile pantherProfile)
        {
            _repository.Update(pantherProfile);
        }

        public void DeletePantherProfile(int id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<PantherViewModel> SearchPantherProfiles(double? weight, string pantherTypeName)
        {
            var query = _repository.GetAll();
            if (weight.HasValue || !string.IsNullOrEmpty(pantherTypeName))
            {
                query = query.Where(lp => (weight.HasValue && lp.Weight == weight.Value) ||
                                          (!string.IsNullOrEmpty(pantherTypeName) && lp.PantherType.PantherTypeName.Contains(pantherTypeName)));
            }
            return query.Select(lp => new PantherViewModel
            {
                Id = lp.PantherTypeId,
                PantherName = lp.PantherName,
                Weight = lp.Weight,
                PantherTypeName = lp.PantherType.PantherTypeName
            }).ToList();
        }

        public IEnumerable<PantherType> GetPantherTypes()
        {
            return _repository.GetPantherTypes().ToList();
        }

        public int GetTotalCount()
        {
            return _repository.GetTotalCount();
        }
    }
}
