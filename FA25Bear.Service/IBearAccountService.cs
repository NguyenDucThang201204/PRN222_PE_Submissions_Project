using FA25Bear.DataAccess.Models;
using FA25Bear.DataAccess.Models.DTOs;

namespace FA25Bear.Service
{
    public interface IBearAccountService
    {
        BearAccount CheckLogin(LoginRequestDTO lr);

    }
}
