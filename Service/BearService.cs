using Repository;
using Repository.Models;
using Service.Model;
using System.Text.RegularExpressions;

namespace Service
{
    public class BearService
    {
        private readonly UnitOfWork _unitOfWork;
        public BearService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<List<BearProfile>> GetAllBearAsync()
        {
            return await _unitOfWork.GetRepository<BearProfile>().GetAllByPropertyAsync(null, "BearType");
        }
        public async Task<BearProfile?> GetBearByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<BearProfile>().GetByPropertyAsync(h => h.BearProfileId == id, true, "BearType");
        }
 
        public async Task AddBearAsync(BearCreateDTO dto)
        {

            string regexPattern = @"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$";
            if (!Regex.IsMatch(dto.BearName, regexPattern))
            {
                throw new ArgumentException();

            }
            if (dto.Weight < 200)
            {
                throw new ArgumentException();
            }
            if (dto.BearName.Length < 4 || dto.BearName.Length > 200)
            {
                throw new ArgumentException();
            }
            BearProfile bear = new();
            bear.BearTypeId = dto.BearTypeId;
            bear.BearName = dto.BearName;
            bear.Characteristics = dto.Characteristics;
            bear.CareNeeds = dto.CareNeeds;
            bear.ModifiedDate = dto.ModifiedDate;
            bear.BearWeight = dto.Weight;

            await _unitOfWork.GetRepository<BearProfile>().AddAsync(bear);
            await _unitOfWork.SaveAsync();
        }
        public async Task UpdateBearAsync(int id, BearCreateDTO dto)
        {
            BearProfile? bear = await _unitOfWork.GetRepository<BearProfile>().GetByPropertyAsync(h => h.BearProfileId == id, true, "BearType");
            if (bear == null)
            {
                throw new ArgumentNullException();
            }
            string regexPattern = @"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$";
            if (!Regex.IsMatch(dto.BearName, regexPattern))
            {
                throw new ArgumentException();

            }
            if (dto.Weight < 200)
            {
                throw new ArgumentException();
            }
            if(dto.BearName.Length < 4 || dto.BearName.Length > 200)
            {
                throw new ArgumentException();
            }
            bear.BearTypeId = dto.BearTypeId;
            bear.BearName = dto.BearName;
            bear.Characteristics = dto.Characteristics;
            bear.CareNeeds = dto.CareNeeds;
            bear.ModifiedDate = dto.ModifiedDate;
            bear.BearWeight = dto.Weight;

            await _unitOfWork.GetRepository<BearProfile>().UpdateAsync(bear);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteBearAsync(int id)
        {
            BearProfile? bear = await _unitOfWork.GetRepository<BearProfile>().GetByPropertyAsync(h => h.BearProfileId == id, true, "BearType");
            if (bear == null)
            {
                throw new ArgumentNullException();
            }
            await _unitOfWork.GetRepository<BearProfile>().DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PaginationResponse> Search(PaginationSearch search)
        {
            var repo = _unitOfWork.GetRepository<BearProfile>();
            var result = new PaginationResponse();
            var items = await repo.GetAllByPropertyAsync(x => x.BearName.Contains(search.BearName) || x.BearWeight == search.BearWeight ||
            x.BearType.BearTypeName.Contains(search.BearName) ,"BearType");

            result.CurrentPage = search.CurrentPage;
            result.PageSize = search.PageSize;
            result.TotalItems = items.Count;
            result.Items = items
                .Skip((search.CurrentPage - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToList();

            return result;
        }
    }
    
}
