using BO;
using BO.Dto;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IBearProfileService
    {
        public BearProfile GetBearProfileById(int id);
        public List<BearProfile> GetBearList();
        public BearProfile CreateBear(BearDto bear);
        public void UpdateBear(int id, BearDto bear);
        public void DeleteBear(int id);
    }
}
