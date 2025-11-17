using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.Models;

namespace Services
{
    public interface IAccountService
    {
        Task<BearAccount> Login(string email, string password);
    }
}
