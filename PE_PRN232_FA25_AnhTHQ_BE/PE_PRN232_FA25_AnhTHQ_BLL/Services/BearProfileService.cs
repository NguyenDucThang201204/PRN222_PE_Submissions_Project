using PE_PRN232_FA25_AnhTHQ_BLL.DTOs;
using PE_PRN232_FA25_AnhTHQ_BLL.DTOs.Request;
using PE_PRN232_FA25_AnhTHQ_BLL.DTOs.Response;
using PE_PRN232_FA25_AnhTHQ_DAL.Models;
using PE_PRN232_FA25_AnhTHQ_DAL.Repostitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_AnhTHQ_BLL.Services
{
    public class BearProfileService
    {
        private readonly BearProfileRepos _bearProfileRepos;
        public BearProfileService(BearProfileRepos bearProfileRepos)
        {
            _bearProfileRepos = bearProfileRepos;
        }

        public async Task<List<BearDto>> GetAllsAsync()
        {
            var items = await _bearProfileRepos.GetAllAsync();

            return items.Select(ToDto).ToList();
        }

        public async Task<BearDto> GetByIdAsync(int id)
        {
            var entity = await _bearProfileRepos.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Profile with ID {id} not found.");

            return ToDto(entity);
        }

        public async Task<PagedResult<BearDto>> SearchAsync(String bearName, int bearWeight, String bearTypeName)
        {
            var items = await _bearProfileRepos.SearchAsync(bearName, bearWeight, bearTypeName);
            var dtoItems = items.Select(ToDto).ToList();
            var pagedResult = new PagedResult<BearDto>
            {
                PageNumber = 1,
                PageSize = dtoItems.Count,
                TotalItems = dtoItems.Count,
                TotalPages = 1,
                Items = dtoItems
            };
            return pagedResult;
        }
        public async Task<BearDto> CreateAsync(CreateProfileRequest e)
        {
            if (e == null) throw new System.ArgumentNullException(nameof(e), "BearProfile cannot be null.");

            var entity = new BearProfile
            {
                BearProfileId = e.BearProfileId,
                BearTypeId = e.BearTypeId,
                BearName = e.BearName,
                BearWeight = e.BearWeight,
                Characteristics = e.Characteristics,
                CareNeeds = e.CareNeeds,
                ModifiedDate = e.ModifiedDate
            };

            await _bearProfileRepos.CreateAsync(entity);
            return ToDto(entity);
        }

        public async Task<BearDto> UpdateAsync(int id, BearDto dto)
        {
            if (dto == null) throw new System.ArgumentNullException(nameof(dto), "BearDto cannot be null.");

            var existing = await _bearProfileRepos.GetByIdAsync(id);
            if (existing == null) throw new KeyNotFoundException($"Profile with ID {id} not found.");

            existing.BearTypeId = dto.BearTypeId;
            existing.BearName = dto.BearName;
            existing.BearWeight = dto.BearWeight;
            existing.Characteristics = dto.Characteristics;
            existing.CareNeeds = dto.CareNeeds;
            existing.ModifiedDate = dto.ModifiedDate;


            existing.BearType = null;

            await _bearProfileRepos.UpdateAsync(existing);
            return ToDto(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _bearProfileRepos.GetByIdAsync(id);
            if (existing == null) throw new KeyNotFoundException($"BearProfile with ID {id} not found.");

            var removed = await _bearProfileRepos.RemoveAsync(existing);
            if (!removed) throw new System.Exception("Failed to delete Profile.");

            return true;
        }

        private static BearDto ToDto(BearProfile e) => new BearDto
        {
            BearProfileId = e.BearProfileId,
            BearTypeId = e.BearTypeId,
            BearName = e.BearName,
            BearWeight = e.BearWeight,
            Characteristics = e.Characteristics,
            CareNeeds = e.CareNeeds,
            ModifiedDate = e.ModifiedDate,
            BearTypeName = e.BearType?.BearTypeName
        };
    }
}
