using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using PE_PRN232_FA25_PhamTrungHieu_BE.DAL.ModelExtensions;
using PE_PRN232_FA25_PhamTrungHieu_BE.DAL.Models;
using PE_PRN232_FA25_PhamTrungHieu_BE.DAL.Repositories;

namespace PE_PRN232_FA25_PhamTrungHieu_BE.BAL.Services
{
    public class BearProfileService : IBearProfileService
    {
        private readonly GenericRepository<BearProfile> _repository;

        public BearProfileService(GenericRepository<BearProfile> repository)
        {
            _repository = repository;
        }

        public async Task<string> CreateAsync(CreateBearProfileRequest request)
        {
            BearProfile bearProfile = new();
            bearProfile.BearTypeId = request.BearTypeId;
            bearProfile.ModifiedDate = request.ModifiedDate;
            bearProfile.CareNeeds = request.CareNeeds;
            bearProfile.BearName = request.BearName;
            bearProfile.Characteristics = request.Characteristics;
            bearProfile.BearWeight = request.BearWeight;

            _ = await _repository.CreateAsync(bearProfile);

            return bearProfile.BearProfileId.ToString();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var bearProfile = await _repository.GetByIdAsync(id);
            if (bearProfile == null)
            {
                return $"BearProfile with id {id} not found";
            }

            _ = await _repository.RemoveAsync(bearProfile);
            return string.Empty;
        }

        public async Task<List<GetBearProfileResponse>> GetAllAsync()
        {
            var bearProfiles = await _repository.FindWithIncludeAsync(
                include: query => query.Include(x => x.BearType));

            var response = bearProfiles.Select(x => new GetBearProfileResponse
            {
                CareNeeds = x.CareNeeds,
                Characteristics = x.Characteristics,
                BearName = x.BearName,
                ModifiedDate = x.ModifiedDate,
                BearProfileId = x.BearProfileId,
                BearWeight = x.BearWeight,
                BearTypeId = x.BearTypeId,
                BearType = new GetBearTypeResponse
                {
                    BearTypeId = x.BearType.BearTypeId,
                    Description = x.BearType.Description,
                    Origin = x.BearType.Origin
                }
            }).OrderByDescending(x => x.BearProfileId).ToList();

            return response;
        }

        public async Task<GetBearProfileResponse> GetByIdAsync(int id)
        {
            var bearProfiles = await _repository.FindWithIncludeAsync(
                predicate: query => query.BearProfileId == id,
                include: query => query.Include(x => x.BearType));

            var response = bearProfiles.Select(x => new GetBearProfileResponse
            {
                CareNeeds = x.CareNeeds,
                Characteristics = x.Characteristics,
                BearName = x.BearName,
                ModifiedDate = x.ModifiedDate,
                BearProfileId = x.BearProfileId,
                BearWeight = x.BearWeight,
                BearTypeId = x.BearTypeId,
                BearType = new GetBearTypeResponse
                {
                    BearTypeId = x.BearType.BearTypeId,
                    Description = x.BearType.Description,
                    Origin = x.BearType.Origin
                }
            }).FirstOrDefault();

            return response;
        }

        public async Task<string> UpdateAsync(int id, UpdateBearProfileRequest request)
        {
            var bearInDb = await _repository.GetByIdAsync(id);
            if (bearInDb == null)
            {
                return $"BearProfile with id {id} not found";
            }

            bearInDb.BearTypeId = request.BearTypeId;
            bearInDb.ModifiedDate = request.ModifiedDate;
            bearInDb.CareNeeds = request.CareNeeds;
            bearInDb.BearName = request.BearName;
            bearInDb.Characteristics = request.Characteristics;
            bearInDb.BearWeight = request.BearWeight;

            _ = await _repository.UpdateAsync(bearInDb);
            return string.Empty;
        }

        public async Task<string> ValidateModelAsync(string bearName, decimal? bearWeight)
        {
            if (!Regex.IsMatch(bearName, @"^([A-Z][a-z]*)(\s[A-Z][a-z]*)*$"))
            {
                return await Task.FromResult("bearName is invalid");
            }

            if (bearWeight <= 200)
            {
                return await Task.FromResult("bearWeight is invalid");
            }

            var length = bearName.Length;
            if (length < 4 || length > 50)
            {
                return await Task.FromResult("bearName must be 4 - 50 characters");
            }

            return await Task.FromResult(string.Empty);
        }
    }
}
