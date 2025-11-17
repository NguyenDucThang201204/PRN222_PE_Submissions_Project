using PE_PRN232_FA25_DaoTrongTien_BE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_DaoTrongTien_BE.DAL.Repositories
{
    public interface IBearProfileRepository
    {

        Task<List<BearProfile>> GetAllBearProfiles();
        Task<BearProfile> GetBearProfileById(int id);
        Task AddBearProfile(BearProfile BearProfile);
        Task UpdateBearProfile(BearProfile BearProfile);
        Task DeleteBearProfile(int id);


    }
}
