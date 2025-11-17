using PE_PRN232_FA25_DaoTrongTien_BE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_DaoTrongTien_BE.BLL.Services
{
    public interface IBearProfileService
    {
        Task<List<BearProfile>> GetAllBearProfiles();
        Task<BearProfile> GetBearProfileById(int id);
        Task AddBearProfile(BearProfile BearProfile);
        Task DeleteBearProfile(int id);
        Task UpdateBearProfile(BearProfile BearProfile);

    }
}
