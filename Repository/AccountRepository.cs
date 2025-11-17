using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.Models;
using DAOs;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<BearAccount> Login(string email, string password)
        {
            return await AccountDAO.Instance.Login(email, password);
        }
    }
}
