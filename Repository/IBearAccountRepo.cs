using BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IBearAccountRepo
    {
        public BearAccount GetSystemAccountByEmail(string email, string pass);
    }
}
