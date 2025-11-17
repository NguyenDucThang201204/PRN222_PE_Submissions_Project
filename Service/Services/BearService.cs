using Repository.Models;
using Repository.Repositories;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services;
public class BearService
{
    private readonly IGenericRepository<BearProfile> _bearProfileRepository;
    private readonly IGenericRepository<BearType> _bearTypeRepository;

    public BearService(IGenericRepository<BearProfile> bearProfileRepository, IGenericRepository<BearType> bearTypeRepository)
    {
        _bearProfileRepository = bearProfileRepository;
        _bearTypeRepository = bearTypeRepository;
    }

    public async Task<IEnumerable<GetResponse>> GetAllBearProfileAsync()
    {
        var bearProfile = await _bearProfileRepository.GetAsync(includeProperties: "BearType");

        return bearProfile.Select(h => new GetResponse
        {
            BearProfileId = h.BearProfileId,
            BearName = h.BearName,
            BearTypeId = h.BearTypeId,
            BearTypeName = h.BearType.BearTypeName,
            BearWeight = h.BearWeight,
            CareNeeds = h.CareNeeds,
            Characteristics = h.Characteristics
        });
    }

    public async Task<GetResponse> GetBearProfileByIdAsync(int id)
    {
        var bearProfiles = await _bearProfileRepository.GetAsync(includeProperties: "BearType");
        var bearProfile = bearProfiles.FirstOrDefault();
        return new GetResponse
        {
            BearProfileId = bearProfile.BearProfileId,
            BearName = bearProfile.BearName,
            BearTypeId = bearProfile.BearTypeId,
            BearTypeName = bearProfile.BearType.BearTypeName,
            BearWeight = bearProfile.BearWeight,
            CareNeeds = bearProfile.CareNeeds,
            Characteristics = bearProfile.Characteristics
        };

    }

    public async Task<bool> CreateBearProfileAsync(CreateRequest request)
    {
        var bearProfile = new BearProfile
        {
            BearName = request.BearName,
            BearTypeId = request.BearTypeId,
            BearWeight = request.BearWeight,
            CareNeeds = request.CareNeeds,
            Characteristics = request.Characteristics
        };
        bearProfile.ModifiedDate = DateTime.UtcNow;
        await _bearProfileRepository.AddAsync(bearProfile);
        await _bearProfileRepository.SaveAsync();
        return true;
    }

    public async Task<bool> UpdateBearProfileAsync(int id, UpdateRequest request)
    {
        var bearProfile = await _bearProfileRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException();

        if (request.BearName != null)
            bearProfile.BearName = request.BearName;

        if (request.BearTypeId != null)
            bearProfile.BearTypeId = request.BearTypeId;

        if (request.BearWeight != null)
            bearProfile.BearWeight = request.BearWeight;

        if (request.CareNeeds != null)
            bearProfile.CareNeeds = request.CareNeeds;

        if (request.Characteristics != null)
            bearProfile.Characteristics = request.Characteristics;

        bearProfile.ModifiedDate = DateTime.UtcNow;
        _bearProfileRepository.Update(bearProfile);
        await _bearProfileRepository.SaveAsync();
        return true;
    }

    public async Task<bool> DeleteBearProfileAsync(int id)
    {
        var bearProfile = await _bearProfileRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException();
        _bearProfileRepository.Delete(bearProfile);
        await _bearProfileRepository.SaveAsync();
        return true;
    }
}
