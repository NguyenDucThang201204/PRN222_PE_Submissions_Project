using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Sevices
{
    public class BearProfileService
    {
        private readonly BearProfileRepository _repository;

        public BearProfileService(BearProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BearProfile>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<BearProfile?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }
        public async Task<(int statusCode, string errorCode, string message, object? data)> CreateAsync(CreateRequest req)
        {


            var BearProfile = new BearProfile
            {

                BearTypeId = req.BearTypeId,
                BearName = req.BearName,
                Weight = req.Weight,
                Characteristics = req.Characteristics,
                CareNeeds = req.CareNeeds,

                ModifiedDate = DateTime.Now,
            };

            try
            {
                var created = await _repository.CreateAsync(BearProfile);
                return (201, "", "Created successfully", created);
            }
            catch
            {
                return (500, "HB50001", "Internal server error", null);
            }
        }
        public async Task<(int statusCode, string errorCode, string message, object? data)> UpdateAsync(int id, CreateRequest req)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return (404, "HB40401", "Resource not found", null);



            existing.BearTypeId = req.BearTypeId;
            existing.BearName = req.BearName;
            existing.Weight = req.Weight;
            existing.Characteristics = req.Characteristics;
            existing.CareNeeds = req.CareNeeds;
            existing.ModifiedDate = req.ModifiedDate;

            try
            {
                var updated = await _repository.UpdateAsync(existing);
                return (200, "", "Updated successfully", updated);
            }
            catch (Exception)
            {
                return (500, "HB50001", "Internal server error", null);
            }
        }
        public async Task<(int statusCode, string errorCode, string message)> DeleteAsync(int id)
        {
            var exists = await _repository.GetByIdAsync(id);
            if (exists == null)
                return (404, "HB40401", "Resource not found");

            try
            {
                await _repository.DeleteAsync(id);
                return (200, "", "Deleted successfully");
            }
            catch (Exception)
            {
                return (500, "HB50001", "Internal server error");
            }
        }
        public async Task<IEnumerable<object>> SearchAsync(string? leopardName, double? weight)
        {
            // Gọi repository để tìm
            var profiles = await _repository.SearchAsync(leopardName, weight);

            // Map sang object trả về (chỉ chứa dữ liệu cần thiết)
            var result = profiles.Select(p => new
            {
                p.BearProfileId,
                p.BearName,
                p.Weight,
                p.Characteristics,
                p.CareNeeds,
                BearType = new
                {
                    p.BearType.BearTypeId,
                    p.BearType.BearTypeName,
                    p.BearType.Origin,
                    p.BearType.Description
                }
            });

            return result;
        }

    }

}
