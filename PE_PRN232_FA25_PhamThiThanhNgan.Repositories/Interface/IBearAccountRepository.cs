
using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Models;

namespace PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Interface
{
    public interface IBearAccountRepository
    {
        Task<BearAccount?> GetAccountByEmailAndPasswordAsync(string email, string password);
    }
}