using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Interfaces
{
    public interface IAccountService
    {
        Task<ClaimsPrincipal> AuthenticateAsync(string email, string password);
    }
}
