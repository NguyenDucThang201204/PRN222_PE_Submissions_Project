using FA25Bear.DataAccess.Models;

namespace FA25Bear.DataAccess.Repo
{
    public class BearProfileRepo : IBearProfileRepo
    {
        private Fa25bearDbContext _dbContext;
        
        public BearProfileRepo(Fa25bearDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddBearProfile(BearProfile bp)
        {
            _dbContext.BearProfiles.Add(bp);
            _dbContext.SaveChanges();
        }

        public void DeleteBearProfile(int id)
        {
            var bearProfile = GetBearProfileById(id);
            if (bearProfile != null)
            {
                _dbContext.BearProfiles.Remove(bearProfile);
                _dbContext.SaveChanges();
            }
        }

        public List<BearProfile> GetAllBearProfiles()
        {
            return _dbContext.BearProfiles.ToList();
        }

        public BearProfile? GetBearProfileById(int id)
        {
            return _dbContext.BearProfiles.FirstOrDefault(h => h.BearProfileId == id);
        }

        public IQueryable<BearProfile> Search(string? modelName, int? weight)
        {
            var query = _dbContext.BearProfiles.AsQueryable();

            if (!string.IsNullOrEmpty(modelName))
                query = query.Where(h => h.BearName.Contains(modelName));

            if (weight != 0)
                query = query.Where(h => h.BearWeight.Equals(weight));

            return query;
        }

        public void UpdateBearProfile(BearProfile bp)
        {
            _dbContext.BearProfiles.Update(bp);
            _dbContext.SaveChanges();
        }
    }
}
