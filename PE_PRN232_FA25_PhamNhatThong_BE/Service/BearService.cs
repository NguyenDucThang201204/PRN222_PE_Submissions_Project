using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.MExtension;
using Repository.Models;
using Service.Dtos;

namespace Service
{
    public class BearService(BearRepository _repo, Fa25bearDbContext _context)
    {
        private static ResponseDto? MapToDto(BearProfile item)
        {
            if (item == null) return null;
            return new ResponseDto
            {
                BearProfileId = item.BearProfileId,
                BearName = item.BearName,
                BearWeight = item.BearWeight,
                Characteristics = item.Characteristics,
                CareNeeds = item.CareNeeds,
                ModifiedDate = item.ModifiedDate,
                BearTypeId = item.BearTypeId,
                BearTypeName = item.BearType.BearTypeName!
            };
        }

        private async Task<int> GenerateNextIdAsync()
        {
            return await _context.BearProfiles.AnyAsync()
                ? await _context.BearProfiles.MaxAsync(h => h.BearProfileId) + 1
                : 1;
        }

        public IQueryable<BearProfile> GetDatas()
        {
            return _repo.GetDatas();
        }

        public async Task<IEnumerable<ResponseDto>> GetAll()
        {
            var items = await _repo.GetAll();
            return items.Select(MapToDto)!;
        }

        public async Task<ResponseDto?> GetById(int id)
        {
            var item = await _repo.GetById(id);
            if (item == null)
            {
                return null;
            }
            return MapToDto(item);
        }

        public async Task<ServiceResult<ResponseDto>> Create(CreateDto dto)
        {
            var sub = await _context.BearTypes.FindAsync(dto.BearTypeId);

            if (sub == null)
            {
                return ServiceResult<ResponseDto>.Fail("H40401", "Type not found");
            }
            int nextId = await GenerateNextIdAsync();
            var item = new BearProfile
            {
                //BearProfileId = nextId,
                BearName = dto.BearName,
                BearWeight = dto.BearWeight,
                Characteristics = dto.Characteristics,
                CareNeeds = dto.CareNeeds,
                BearTypeId = dto.BearTypeId,
                ModifiedDate = dto.ModifiedDate
            };
            var created = await _repo.Create(item);

            return ServiceResult<ResponseDto>.Ok(MapToDto(created)!);
        }

        public async Task<ServiceResult<ResponseDto>> Update(int id, UpdateDto dto)
        {
            var sub = await _context.BearTypes.FindAsync(dto.BearTypeId);
            if (sub == null)
            {
                return ServiceResult<ResponseDto>.Fail("H40401", "Type not found");
            }
            var item = await _repo.GetById(id);
            if (item == null)
            {
                return ServiceResult<ResponseDto>.Fail("H40401", "Profile not found");
            }
            item.BearName = dto.BearName;
            item.BearWeight = dto.BearWeight;
            item.Characteristics = dto.Characteristics;
            item.CareNeeds = dto.CareNeeds;
            item.ModifiedDate = dto.ModifiedDate;
            item.BearTypeId = dto.BearTypeId;

            var updated = await _repo.Update(item);
            return ServiceResult<ResponseDto>.Ok(MapToDto(updated!)!);
        }

        public async Task<ServiceResult<bool>> Delete(int id)
        {
            var item = await _repo.GetById(id);
            if (item == null)
            {
                return ServiceResult<bool>.Fail("H40401", "Profile not found");
            }
            await _repo.Delete(id);
            return ServiceResult<bool>.Ok(true);
        }

        public async Task<PaginationResult<ResponseDto>> GetAllPaging(int page, int pageSize)
        {
            var pagedResult = await _repo.GetAllPaging(page, pageSize);

            return new PaginationResult<ResponseDto>
            {
                TotalItems = pagedResult.TotalItems,
                TotalPages = pagedResult.TotalPages,
                CurrentPage = pagedResult.CurrentPage,
                PageSize = pagedResult.PageSize,
                Items = pagedResult.Items.Select(MapToDto).ToList()!
            };
        }

        public async Task<IEnumerable<SearchGroup>> SearchGroup(string? search1, double? search2, string? search3)
        {
            var items = await _repo.SearchGroup(search1, search2, search3);
            var grouped = items
                .GroupBy(h => h.BearType.BearTypeName)
                .Select(g => new SearchGroup
                {
                    BearTypeName = g.Key!,
                    Items = g.Select(MapToDto).ToList()!
                });
            return grouped;
        }


        public async Task<IEnumerable<ResponseDto>> Search(string? search1, double? search2, string? search3)
        {
            var items = await _repo.Search(search1, search2, search3);
            return items.Select(MapToDto)!;
        }

        public async Task<PaginationResult<ResponseDto>> SearchWithPaging(int page, int pageSize, string? search1, double? search2, string? search3)
        {
            var list = await _repo.Search(search1, search2, search3);
            var items = list.Select(MapToDto)!
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new PaginationResult<ResponseDto>
            {
                TotalItems = list.Count(),
                TotalPages = (int)Math.Ceiling(list.Count() / (double)pageSize),
                CurrentPage = page,
                PageSize = pageSize,
                Items = items!
            };

        }
    }
}