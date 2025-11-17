using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string email, string roleID);
        ClaimsPrincipal ValidateToken(string token);
        string GenerateRefreshToken();
    }
}
