using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.ModelExtensions;
using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Models;
using Practice_FA25_PE_Services.DTO;

namespace Practice_FA25_PE_Services.Interface
{
    public interface IBearProfileService
    {
        Task<IEnumerable<BearProfileDTO>> GetAllProfilesAsync();
        Task<BearProfileDTO?> GetProfileByIdAsync(int id);
        IQueryable<BearProfileDTO> GetQueryableProfiles();
        Task<BearProfileDTO> CreateProfileAsync(BearProfileRequestDTO profileDto);
        Task<BearProfileDTO?> UpdateProfileAsync(int id, BearProfileRequestDTO profileDto);
        Task<bool> DeleteProfileAsync(int id);
        Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(BearSearchRequest searchrequest);
    }
}
