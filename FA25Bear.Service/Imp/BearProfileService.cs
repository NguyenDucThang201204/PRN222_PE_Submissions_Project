using FA25Bear.DataAccess;
using FA25Bear.DataAccess.Models;
using FA25Bear.DataAccess.Models.DTOs;
using System.Text.RegularExpressions;

namespace FA25Bear.Service.Imp
{
    public class BearProfileService : IBearProfileService
    {
        private readonly IBearProfileRepo _repo;
        public BearProfileService (IBearProfileRepo repo)
        {
            _repo = repo;
        }
        public void Create(BearProfileDTO bfdto)
        {
            if (bfdto == null)
                throw new ArgumentNullException(nameof(bfdto), "Bear Profile cannot be null.");

            // validation
            var namePattern = @"^[A-Za-z0-9\s]{4,50}$";

            if (string.IsNullOrWhiteSpace(bfdto.BearName))
                throw new ArgumentException("Bear name is required.", nameof(bfdto.BearName));

            if (!Regex.IsMatch(bfdto.BearName, namePattern))
                throw new ArgumentException("Bear name must be 3–50 characters long and contain only letters, numbers, and spaces (no special characters).", nameof(bfdto.BearName));

            if (bfdto.BearWeight <= 200)
                throw new ArgumentException("Bear weight much above 200.", nameof(bfdto.BearWeight));

            // mapping DTO to entity
            var bp = new BearProfile();
            bp.BearTypeId = bfdto.BearTypeId;
            bp.BearName = bfdto.BearName;
            bp.BearWeight = bfdto.BearWeight;
            bp.Characteristics = bfdto.Characteristics;
            bp.CareNeeds = bfdto.CareNeeds;
            bp.ModifiedDate = bfdto.ModifiedDate;

            _repo.AddBearProfile(bp);
        }

        public void Delete(int id)
        {
            _repo.DeleteBearProfile(id);
        }

        public List<BearProfile> GetAll()
        {
            return _repo.GetAllBearProfiles();
        }

        public BearProfile GetById(int id)
        {
            return _repo.GetBearProfileById(id);
        }

        public IQueryable<BearProfile> Search(string? modelName, int? weight)
        {
            return _repo.Search(modelName, weight);

        }

        public void Update(BearProfile bf)
        {
            _repo.UpdateBearProfile(bf);
        }
    }
}
