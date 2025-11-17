using PE_PRN232_FA25_LeCongHung_BLL.DTOs;

namespace PE_PRN232_FA25_LeCongHung_BLL.Services
{
    public interface IBearProfileService
    {
        Task<BearProfileDTO?> CreateAsync(BearProfileCreateDTO dto);
        Task<BearProfileDTO?> UpdateAsync(BearProfileUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<List<BearProfileDTO>> GetAllAsync();
        Task<BearProfileDTO?> GetByIdAsync(int id);
        Task<SearchResponseDTO<BearProfileDTO>> SearchAsync(SearchRequestDTO request);
    }
}

