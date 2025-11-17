using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.Models;

namespace Repository
{
    public interface IAccountRepository
    {
        Task<BearAccount> Login(string email, string password);
    }
}
