using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class AccountRepo : IAccountRepo
    {
        private readonly Fa25bearDbContext _db;
        private static AccountRepo _instance;
        public AccountRepo()
        {
            _db = new Fa25bearDbContext();
        }
        public static AccountRepo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AccountRepo();
                }
                return _instance;
            }
        }
        public BearAccount GetAccountByEmail(string email, string pass)
        {
            return _db.BearAccounts.FirstOrDefault(a => a.Email.Equals(email) && a.Password.Equals(pass));
        }
    }
}
