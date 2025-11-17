using BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.DTO.BearDTOs;

namespace Repo
{
    public class BearRepo : IBearRepo
    {
        private readonly Fa25bearDbContext _db;
        public BearRepo()
        {
            _db = new Fa25bearDbContext();
        }
        public BearResponseDto Add(BearProfile newBearProfile)
        {
            try
            {
                _db.BearProfiles.Add(newBearProfile);
                _db.SaveChanges();
                return GetById(newBearProfile.BearProfileId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool Delete(int BearProfileId)
        {
            try
            {
                BearProfile bearToDelete = GetByIdForUpdate(BearProfileId);
                if (bearToDelete != null)
                {
                    _db.BearProfiles.Remove(bearToDelete);
                    _db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<BearResponseDto> GetAll()
        {
            return _db.BearProfiles
                .Include(h => h.BearType)
                .Select(h => new BearResponseDto
                {
                    BearProfileId = h.BearProfileId,
                    BearName = h.BearName,
                    BearWeight = h.BearWeight,
                    Characteristics = h.Characteristics,
                    CareNeeds = h.CareNeeds,
                    ModifiedDate = h.ModifiedDate,
                    BearTypeName = h.BearType != null ? h.BearType.BearTypeName : string.Empty,
                    Origin = h.BearType != null ? h.BearType.Origin : null,
                    Description = h.BearType != null ? h.BearType.Description : null,
                })
                .OrderByDescending(b => b.ModifiedDate).ToList();
        }
        public BearResponseDto? GetById(int BearProfile)
        {
            return _db.BearProfiles
                .Include(h => h.BearType)
                .Select(h => new BearResponseDto
                {
                    BearProfileId = h.BearProfileId,
                    BearName = h.BearName,
                    BearWeight = h.BearWeight,
                    Characteristics = h.Characteristics,
                    CareNeeds = h.CareNeeds,
                    ModifiedDate = h.ModifiedDate,
                    BearTypeName = h.BearType != null ? h.BearType.BearTypeName : string.Empty,
                    Origin = h.BearType != null ? h.BearType.Origin : null,
                    Description = h.BearType != null ? h.BearType.Description : null,
                })
                .FirstOrDefault(b => b.BearProfileId == BearProfile);
        }
        public BearProfile GetByIdForUpdate(int BearProfileId)
        {
            return _db.BearProfiles.FirstOrDefault(b => b.BearProfileId == BearProfileId);
        }

        public BearType GetByIdType(int typeId)
        {
            return _db.BearTypes.FirstOrDefault(bt => bt.BearTypeId == typeId);
        }

        public List<BearResponseDto> Search(string name, int weight, string type)
        {
            //if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(material))
            //{
            //    return GetAll();
            //}
            //return _db.BearProfiles
            //    .Include(h => h.BearType)
            //    .Select(h => new BearResponseDto
            //    {
            //        BearProfileId = h.BearProfileId,
            //        BearName = h.BearName,
            //        BearWeight = h.BearWeight,
            //        Characteristics = h.Characteristics,
            //        CareNeeds = h.CareNeeds,
            //        ModifiedDate = h.ModifiedDate,
            //        BearTypeName = h.BearType != null ? h.BearType.BearTypeName : string.Empty,
            //        Origin = h.BearType != null ? h.BearType.Origin : null,
            //        Description = h.BearType != null ? h.BearType.Description : null,
            //    })
            //    .Where(h => (string.IsNullOrEmpty(modelName) || h.ModelName.Contains(modelName)) &&
            //                (string.IsNullOrEmpty(material) || h.Material.Contains(material)))
            //    .ToList();
            return GetAll();
        }
        public BearResponseDto Update(BearProfile updatedBearProfile)
        {
            try
            {
                BearProfile existed = GetByIdForUpdate(updatedBearProfile.BearProfileId);
                if (existed != null)
                {
                    existed.BearName = updatedBearProfile.BearName;
                    existed.BearTypeId = updatedBearProfile.BearTypeId;
                    existed.BearWeight = updatedBearProfile.BearWeight;
                    existed.Characteristics = updatedBearProfile.Characteristics;
                    existed.CareNeeds = updatedBearProfile.CareNeeds;
                    existed.ModifiedDate = DateTime.Now;
                    _db.SaveChanges();
                    return GetById(existed.BearProfileId);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
