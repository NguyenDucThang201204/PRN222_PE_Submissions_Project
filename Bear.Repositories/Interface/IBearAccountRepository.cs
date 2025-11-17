using Bear.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bear.Repositories.Interface
{
    public interface IBearAccountRepository
    {
        Task<BearAccount> GetAccountByEmail(string email, string password);
    }
}
