using Microsoft.EntityFrameworkCore;
using PE_PRN232_FA25_LeCongHung_BLL.DTOs;
using PE_PRN232_FA25_LeCongHung_DAL.Entities;
using PE_PRN232_FA25_LeCongHung_DAL.Repositories;

namespace PE_PRN232_FA25_LeCongHung_BLL.Services
{
    public class BearProfileService : IBearProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BearProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BearProfileDTO?> CreateAsync(BearProfileCreateDTO dto)
        {
            // Validate BearName
            if (!ValidationService.ValidateBearName(dto.BearName, out string bearNameError))
            {
                throw new ArgumentException(bearNameError);
            }

            // Validate BearWeight
            if (!ValidationService.ValidateBearWeight(dto.BearWeight, out string weightError))
            {
                throw new ArgumentException(weightError);
            }

            var entity = new BearProfile
            {
                BearTypeId = dto.BearTypeId,
                BearName = dto.BearName.Trim(),
                BearWeight = dto.BearWeight,
                Characteristics = dto.Characteristics?.Trim(),
                CareNeeds = dto.CareNeeds?.Trim(),
                ModifiedDate = DateTime.Now
            };

            _unitOfWork.BearProfileRepository.Add(entity);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(entity.BearProfileId);
        }

        public async Task<BearProfileDTO?> UpdateAsync(BearProfileUpdateDTO dto)
        {
            var entity = _unitOfWork.BearProfileRepository.GetById(dto.BearProfileId);
            if (entity == null) return null;

            // Validate BearName
            if (!ValidationService.ValidateBearName(dto.BearName, out string bearNameError))
            {
                throw new ArgumentException(bearNameError);
            }

            // Validate BearWeight
            if (!ValidationService.ValidateBearWeight(dto.BearWeight, out string weightError))
            {
                throw new ArgumentException(weightError);
            }

            entity.BearTypeId = dto.BearTypeId;
            entity.BearName = dto.BearName.Trim();
            entity.BearWeight = dto.BearWeight;
            entity.Characteristics = dto.Characteristics?.Trim();
            entity.CareNeeds = dto.CareNeeds?.Trim();
            entity.ModifiedDate = DateTime.Now;

            _unitOfWork.BearProfileRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(entity.BearProfileId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = _unitOfWork.BearProfileRepository.GetById(id);
            if (entity == null) return false;

            _unitOfWork.BearProfileRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<BearProfileDTO>> GetAllAsync()
        {
            var query = _unitOfWork.BearProfileRepository.GetAll()
                .Include(bp => bp.BearType)
                .OrderByDescending(bp => bp.BearProfileId); // New items at the top

            var entities = await query.ToListAsync();

            return entities.Select(e => new BearProfileDTO
            {
                BearProfileId = e.BearProfileId,
                BearTypeId = e.BearTypeId,
                BearName = e.BearName,
                BearWeight = e.BearWeight,
                Characteristics = e.Characteristics,
                CareNeeds = e.CareNeeds,
                ModifiedDate = e.ModifiedDate,
                BearTypeName = e.BearType?.BearTypeName
            }).ToList();
        }

        public async Task<BearProfileDTO?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.BearProfileRepository.GetAll()
                .Include(bp => bp.BearType)
                .FirstOrDefaultAsync(bp => bp.BearProfileId == id);

            if (entity == null) return null;

            return new BearProfileDTO
            {
                BearProfileId = entity.BearProfileId,
                BearTypeId = entity.BearTypeId,
                BearName = entity.BearName,
                BearWeight = entity.BearWeight,
                Characteristics = entity.Characteristics,
                CareNeeds = entity.CareNeeds,
                ModifiedDate = entity.ModifiedDate,
                BearTypeName = entity.BearType?.BearTypeName
            };
        }

        public async Task<SearchResponseDTO<BearProfileDTO>> SearchAsync(SearchRequestDTO request)
        {
            var query = _unitOfWork.BearProfileRepository.GetAll()
                .Include(bp => bp.BearType)
                .AsQueryable();

            // Search by BearName (relative search)
            if (!string.IsNullOrWhiteSpace(request.BearName))
            {
                query = query.Where(bp => bp.BearName.Contains(request.BearName));
            }

            // Search by BearWeight
            if (request.BearWeight.HasValue)
            {
                query = query.Where(bp => bp.BearWeight == request.BearWeight);
            }

            // Search by BearTypeName (relative search)
            if (!string.IsNullOrWhiteSpace(request.BearTypeName))
            {
                query = query.Where(bp => bp.BearType != null && 
                    bp.BearType.BearTypeName.Contains(request.BearTypeName));
            }

            // Order by newest first
            query = query.OrderByDescending(bp => bp.BearProfileId);

            // Get total count
            var totalItems = await query.CountAsync();

            // Calculate pagination
            var totalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize);
            var currentPage = request.CurrentPage;
            var skip = (currentPage - 1) * request.PageSize;

            // Get paged results
            var entities = await query.Skip(skip).Take(request.PageSize).ToListAsync();

            var items = entities.Select(e => new BearProfileDTO
            {
                BearProfileId = e.BearProfileId,
                BearTypeId = e.BearTypeId,
                BearName = e.BearName,
                BearWeight = e.BearWeight,
                Characteristics = e.Characteristics,
                CareNeeds = e.CareNeeds,
                ModifiedDate = e.ModifiedDate,
                BearTypeName = e.BearType?.BearTypeName
            }).ToList();

            return new SearchResponseDTO<BearProfileDTO>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = request.PageSize,
                Items = items
            };
        }
    }
}

