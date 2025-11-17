using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IBearService
    {
        Task<IEnumerable<BearProfile>> GetAllAsync();
    }
}
