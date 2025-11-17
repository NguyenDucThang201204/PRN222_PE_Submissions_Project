using BOs;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BearAccountRepo : IBearAccountRepo
    {
        public BearAccount GetSystemAccountByEmail(string email, string pass)
        {
            return BearAccountDAO.Instance.GetBearAccountByEmail(email, pass);
        }
    }
}
