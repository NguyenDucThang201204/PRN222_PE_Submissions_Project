using Repository.Models;

namespace Service
{
    public interface IAuthService
    {
        Task<BearAccount> Login(string userName, string password);
    }
}