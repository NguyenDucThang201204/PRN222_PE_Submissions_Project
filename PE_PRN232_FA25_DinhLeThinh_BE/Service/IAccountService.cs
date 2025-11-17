using Model;
using Model.DTOs;

namespace Service
{
    public interface IAccountService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
    }
}
