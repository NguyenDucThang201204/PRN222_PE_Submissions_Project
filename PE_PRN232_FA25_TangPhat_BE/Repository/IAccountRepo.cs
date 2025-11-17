using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IAccountRepo
    {
        public Task<BearAccount?> LoginAsync(string email, string pass);
    }
}
