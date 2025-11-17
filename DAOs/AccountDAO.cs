using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.Models;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class AccountDAO
    {
        private static AccountDAO instance = null;
        private readonly Fa25bearDbContext context;
        private AccountDAO()
        {
            context = new Fa25bearDbContext();
        }
        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }
        public async Task<BearAccount> Login(string email, string password)
        {
            var account = await context.BearAccounts.FirstOrDefaultAsync(account => account.Email == email && account.Password == password);
            return account;
        }
    }
}
