using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IBearProfileRepository
    {
        public BearProfile GetBearProfileById(int id);
        public List<BearProfile> GetBearList();
        public BearProfile CreateBear(BearProfile bear);
        public void UpdateBear(BearProfile bear);
        public void DeleteBear(int id);

    }
}
