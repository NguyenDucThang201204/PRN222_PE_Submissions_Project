using Repositories.ThienNH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ThienNH
{
    public interface IBearTypeService
    {
        Task<List<BearType>> GetAllAsync();
        Task<BearType> GetByIdAsync(int id);
    }
}
