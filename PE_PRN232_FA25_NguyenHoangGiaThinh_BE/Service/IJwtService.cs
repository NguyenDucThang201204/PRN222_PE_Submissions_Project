using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IJwtService
    {
        string GenerateToken(string serId, string email, IList<string> roles = null);
        ClaimsPrincipal ValidateToken(string token);
        
    }
}
