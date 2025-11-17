using PE_PRN232_FA25_PhamTrungHieu_BE.DAL.ModelExtensions;

namespace PE_PRN232_FA25_PhamTrungHieu_BE.BAL.Services
{
    public interface IBearProfileService
    {
        Task<GetBearProfileResponse> GetByIdAsync(int id);
        Task<List<GetBearProfileResponse>> GetAllAsync();
        Task<string> CreateAsync(CreateBearProfileRequest request);
        Task<string> UpdateAsync(int id, UpdateBearProfileRequest request);
        Task<string> DeleteAsync(int id);
        Task<string> ValidateModelAsync(string bearName, decimal? bearWeight);
    }
}
