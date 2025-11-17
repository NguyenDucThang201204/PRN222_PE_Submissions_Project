using FA25Bear.DataAccess.Models;

namespace FA25Bear.DataAccess
{
    public interface IBearProfileRepo
    {
        List<BearProfile> GetAllBearProfiles();
        BearProfile? GetBearProfileById(int id);
        void AddBearProfile(BearProfile bp);
        void UpdateBearProfile(BearProfile bp);
        void DeleteBearProfile(int id);
        IQueryable<BearProfile> Search(string? modelName, int? weight);
    }
}
