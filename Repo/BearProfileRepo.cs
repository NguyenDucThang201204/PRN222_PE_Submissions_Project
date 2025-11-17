using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class BearProfileRepo
    {
        private readonly BearProfileDAO _dao;
        public BearProfileRepo(BearProfileDAO dao)
        {
            _dao = dao;
        }
        public List<BearProfile> GetAllBears()
        {
            return _dao.GetAllBears();
        }
        public BearProfile GetBearsById(int id)
        {
            return _dao.GetBearsById(id);
        }
        public void AddBear(BearProfile bear)
        {
            _dao.AddBear(bear);
        }
        public void UpdateBear(BearProfile bear)
        {
            _dao.UpdateBear(bear);
        }
        public void DeleteBear(int id)
        {
            _dao.DeleteBear(id);
        }

        //public IQueryable<BearProfile> SearchHandBags(string searchTerm)
        //{
        //    return _dao.SearchHandBags(searchTerm);
        //}

        //public IEnumerable<object> SearchHandBagsGrouped(string searchTerm)
        //{
        //    return _dao.SearchHandBagsGrouped(searchTerm);
        //}
    }
}
