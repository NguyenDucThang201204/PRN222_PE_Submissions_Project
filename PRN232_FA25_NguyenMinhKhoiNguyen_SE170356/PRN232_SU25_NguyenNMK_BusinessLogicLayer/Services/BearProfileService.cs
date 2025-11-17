using PRN232_SU25_NguyenNMK_DataAccessLayer.DTOs;
using PRN232_SU25_NguyenNMK_DataAccessLayer.Models;
using PRN232_SU25_NguyenNMK_DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PRN232_SU25_NguyenNMK_BusinessLogicLayer.Services
{
    public class BearProfileService : IBearProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BearProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private bool IsValidItemName(string itemName) =>
            !string.IsNullOrEmpty(itemName) && Regex.IsMatch(itemName, @"^[A-Za-z0-9\s#]+$");

        private async Task ValidateBearDTO(BearCreateUpdateDTO dto)
        {
            if (string.IsNullOrEmpty(dto.BearName) || !IsValidItemName(dto.BearName))
                throw new Exception("Invalid itemName");
            if (dto.BearWeight <= 200)
                throw new Exception("Price must be greater than 200");
            if (await _unitOfWork.BearTypeRepository.GetByIdAsync(dto.BearTypeId) == null)
                throw new Exception("Invalid BearTypeId");
        }

        private BearDTO MapToBearDTO(BearProfile bear) => new BearDTO
        {
            BearProfileId = bear.BearProfileId,
            BearName = bear.BearName ?? string.Empty,
            Characteristics = bear.Characteristics ?? string.Empty,
            BearWeight = bear.BearWeight,
            CareNeeds = bear.CareNeeds ?? string.Empty,
            ModifiedDate = bear.ModifiedDate,

            BearTypeId = bear.BearTypeId ,
            BearTypeName = bear.BearType?.BearTypeName ?? "Unknown Brand",
            Origin = bear.BearType?.Origin,
            Description = bear.BearType?.Description,
        };

        private IQueryable<BearProfile> ApplyFilters(IQueryable<BearProfile> query, string bearName, string bearTypeName)
        {
            if (!string.IsNullOrEmpty(bearName))
                query = query.Where(h => h.BearName != null && h.BearName.Contains(bearName));
            if (!string.IsNullOrEmpty(bearTypeName))
                query = query.Where(h => h.BearType.BearTypeName != null && h.BearType.BearTypeName.Contains(bearTypeName));
            return query;
        }

        public async Task<IEnumerable<BearDTO>> GetAllAsync(string bearName = null, string bearTypeName = null)
        {
            var query = await _unitOfWork.BearProfileRepository.GetAllAsync();
            return ApplyFilters(query.AsQueryable(), bearName, bearTypeName)
                .Select(MapToBearDTO)
                .ToList();
        }

        public async Task<BearDTO> GetByIdAsync(int id)
        {
            var handbag = await _unitOfWork.BearProfileRepository.GetByIdAsync(id)
                ?? throw new Exception("Bear not found");
            return MapToBearDTO(handbag);
        }

        public async Task<BearDTO> CreateAsync(BearCreateUpdateDTO dto)
        {
            await ValidateBearDTO(dto);

            if (dto.BearProfileId.HasValue)
            {
                if (dto.BearProfileId.Value < 0)
                    throw new Exception("BearId can be positive");
                if (await _unitOfWork.BearProfileRepository.GetByIdAsync(dto.BearProfileId.Value) != null)
                    throw new Exception("BearId already exists");
            }

            var bear = new BearProfile
            {
                BearProfileId = dto.BearProfileId ?? (await _unitOfWork.BearProfileRepository.GetAllAsync()).Count() + 1,
                BearName = dto.BearName,
                BearWeight = dto.BearWeight,
                Characteristics = dto.Characteristics,
                CareNeeds = dto.CareNeeds,
                BearTypeId = dto.BearTypeId,
                ModifiedDate = DateTime.Now,
            };

            await _unitOfWork.BearProfileRepository.AddAsync(bear);
            await _unitOfWork.SaveChangesAsync();

            return MapToBearDTO(await _unitOfWork.BearProfileRepository.GetByIdAsync(bear.BearProfileId));
        }

        public async Task<BearDTO> UpdateAsync(int id, BearCreateUpdateDTO dto)
        {
            var bear = await _unitOfWork.BearProfileRepository.GetByIdAsync(id)
                ?? throw new Exception("Bear not found");

            await ValidateBearDTO(dto);

            if (dto.BearProfileId.HasValue && dto.BearProfileId.Value != id)
                throw new Exception("BearId cannot be changed during update");

            bear.BearName = dto.BearName;
            bear.BearWeight = dto.BearWeight;
            bear.Characteristics = dto.Characteristics;
            bear.CareNeeds = dto.CareNeeds;
            bear.BearTypeId = dto.BearTypeId;
            bear.ModifiedDate = DateTime.Now;

            await _unitOfWork.BearProfileRepository.UpdateAsync(bear);
            await _unitOfWork.SaveChangesAsync();

            return MapToBearDTO(bear);
        }

        public async Task DeleteAsync(int id)
        {
            if (await _unitOfWork.BearProfileRepository.GetByIdAsync(id) == null)
                throw new Exception("Bear not found");

            await _unitOfWork.BearProfileRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<IGrouping<string, BearDTO>>> SearchAsync(string bearName, string bearTypeName)
        {
            var query = await _unitOfWork.BearProfileRepository.GetAllAsync();
            return ApplyFilters(query.AsQueryable(), bearName, bearTypeName)
                .Select(MapToBearDTO)
                .ToList()
                .GroupBy(h => h.BearTypeName);
        }
    }
}
