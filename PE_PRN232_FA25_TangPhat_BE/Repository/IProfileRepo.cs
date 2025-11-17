using BusinessObjects;
using BusinessObjects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IProfileRepo
    {
        Task<List<GetDTO>> GetAllAsync();

        Task<GetDTO?> GetById(int id);

        Task<GetDTO?> CreateAsync(BearProfile profile);

        Task<BearType?> GetTypeById(int id);

        Task<BearProfile?> GetByIdForUpdate(int id);

        Task<GetDTO?> UpdateAsync(BearProfile profile);

        Task<bool> DeleteAsync(int id);

    }
}

