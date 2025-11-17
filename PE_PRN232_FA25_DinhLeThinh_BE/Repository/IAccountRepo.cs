using Model;

namespace Repository
{
    public interface IAccountRepo
    {
        Task<BearAccount?> LoginAsync(string email, string pass);
    }
}
