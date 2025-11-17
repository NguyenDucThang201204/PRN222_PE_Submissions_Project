using PE_PRN232_FA25_PhamTrungHieu_BE.DAL.ModelExtensions;

namespace PE_PRN232_FA25_PhamTrungHieu_BE.BAL.Services
{
    public interface IBearAccountService
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    }
}
