using BO;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BearAccountRepository : IBearAccountRepository
    {
        private readonly BearAccountDAO _account;

        public BearAccountRepository (BearAccountDAO account)
        {
            _account = account;
        }
        public BearAccount GetBearAccount(string email, string password) => _account.GetBearAccount(email, password);
        
    }
}
