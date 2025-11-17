using Bear.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bear.Services.Interface
{
    public interface IBearAccountService
    {
        Task<BearAccountDTO> GetAccountByEmail(string email, string password);
    }

}
