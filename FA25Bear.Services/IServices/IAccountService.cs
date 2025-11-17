using FA25Bear.Repositories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA25Bear.Services.IServices
{
    public interface IAccountService
    {
        AuthResponse Authenticate(string email, string password);
    }
}
