using PRN232_SU25_NguyenNMK_DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_NguyenNMK_BusinessLogicLayer.Services
{
    public interface IBearProfileService
    {
        Task<IEnumerable<BearDTO>> GetAllAsync(string productName = null, string material = null);
        Task<BearDTO> GetByIdAsync(int id);
        Task<BearDTO> CreateAsync(BearCreateUpdateDTO dto);
        Task<BearDTO> UpdateAsync(int id, BearCreateUpdateDTO dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<IGrouping<string, BearDTO>>> SearchAsync(string productName, string material);
    }
}
