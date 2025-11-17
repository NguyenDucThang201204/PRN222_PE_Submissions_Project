using PE_PRN232_FA25_LeCongHung_BLL.DTOs;

namespace PE_PRN232_FA25_LeCongHung_BLL.Services
{
    public interface IAccountService
    {
        Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request);
    }
}

