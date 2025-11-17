using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class BearProfileService : IBearProfileService
    {
        private readonly IBearProfileRepository _repo;
        public BearProfileService(IBearProfileRepository repo) => _repo = repo;

        public IEnumerable<BearProfile> GetAll() => _repo.GetAll();
        public BearProfile? GetById(int id) => _repo.GetById(id);

        public void Add(BearProfile bear)
        {
            bear.ModifiedDate = DateTime.Now;
            _repo.Add(bear);
            _repo.Save();
        }

        public void Update(int id, BearProfile bear)
        {
            var existing = _repo.GetById(id);
            if (existing == null) return;

            existing.BearTypeId = bear.BearTypeId;
            existing.BearName = bear.BearName;
            existing.BearWeight = bear.BearWeight;
            existing.Characteristics = bear.Characteristics;
            existing.CareNeeds = bear.CareNeeds;
            existing.ModifiedDate = DateTime.Now;

            _repo.Update(existing);
            _repo.Save();
        }

        public void Delete(int id)
        {
            var bear = _repo.GetById(id);
            if (bear == null) return;
            _repo.Delete(bear);
            _repo.Save();
        }
    }
}
