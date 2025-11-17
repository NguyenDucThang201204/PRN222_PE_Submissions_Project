using FA25Bear.DataAccess.Models;

namespace FA25Bear.DataAccess
{
    public interface IBearAccountRepo
    {
        BearAccount? GetSystemAccountByEmaild(string email, string password);

    }
}
