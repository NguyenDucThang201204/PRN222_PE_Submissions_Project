using BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BearAccountDAO
    {
        private static Fa25bearDbContext db;
        private static BearAccountDAO instance;
        public BearAccountDAO() { 
            db = new Fa25bearDbContext();
        }
        public static BearAccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BearAccountDAO();
                }
                return instance;
            }
        }
        public BearAccount GetBearAccountByEmail(string email, string pass)
        {
            return db.BearAccounts.SingleOrDefault(m=>m.Email.Equals(email) && m.Password.Equals(pass));
        }
    }
}
