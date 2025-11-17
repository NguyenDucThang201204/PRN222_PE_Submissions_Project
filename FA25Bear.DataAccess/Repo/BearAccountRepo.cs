using FA25Bear.DataAccess.Models;

namespace FA25Bear.DataAccess.Repo
{
    public class BearAccountRepo : IBearAccountRepo
    {
        private readonly Fa25bearDbContext _dbContext;

        public BearAccountRepo(Fa25bearDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BearAccount? GetSystemAccountByEmaild(string email, string password)
        {
            return _dbContext.BearAccounts.FirstOrDefault(sa => sa.Email == email && sa.Password == password);
        }
    }
}
