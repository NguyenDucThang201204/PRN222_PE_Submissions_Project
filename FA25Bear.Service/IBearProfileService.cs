using FA25Bear.DataAccess.Models;
using FA25Bear.DataAccess.Models.DTOs;

namespace FA25Bear.Service
{
    public interface IBearProfileService
    {
        List<BearProfile> GetAll();
        BearProfile GetById(int id);
        void Create(BearProfileDTO bfdto);
        void Update(BearProfile bf);
        void Delete(int id);
        IQueryable<BearProfile> Search(string? modelName, int? weight);
    }
}
