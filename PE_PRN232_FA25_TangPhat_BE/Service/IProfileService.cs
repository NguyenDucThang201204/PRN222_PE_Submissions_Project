using BusinessObjects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IProfileService
    {
        Task<List<GetDTO>> GetAllAsync();

        Task<GetDTO?> GetByIdAsync(int id);

        Task<GetDTO?> CreateAsync(CreateDTO request);

        Task<GetDTO?> UpdateAsync(int id, CreateDTO request);

        Task<bool> DeleteAsync(int id);

    }
}
