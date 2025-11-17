using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Interface
{
    public interface IBearTypeRepository
    {
        Task<IEnumerable<BearType>> GetAllAsync();
        Task<BearType?> GetByIdAsync(int id);
    }
}
