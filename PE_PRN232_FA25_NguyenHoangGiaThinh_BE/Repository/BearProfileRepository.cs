using BO;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BearProfileRepository : IBearProfileRepository
    {
        private readonly BearProfileDAO _Dao;

        public BearProfileRepository(BearProfileDAO dao)
        {
            _Dao = dao;
        }
        public BearProfile CreateBear(BearProfile bear)
        => _Dao.CreateBear(bear);

        public void DeleteBear(int id)=> _Dao.DeleteBear(id);
        

        public List<BearProfile> GetBearList()
        => _Dao.GetBearList();

        public BearProfile GetBearProfileById(int id)
        =>_Dao.GetBearProfileById(id);

        public void UpdateBear(BearProfile bear)
        => _Dao.UpdateBear(bear);
    }
}
