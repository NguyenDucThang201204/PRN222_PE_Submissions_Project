using DAL;
using DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProfileService
    {
        private readonly ProfileRepo _repo;

        public ProfileService(ProfileRepo repo)
        {
            _repo = repo;
        }

        public List<BearProfile> GetAll()
        {
            return _repo.GetAll();
        }

        public BearProfile? GetById(int id)
        {
            return _repo.GetById(id);
        }

        public bool Add(BearProfile item)
        {
            return _repo.Add(item);
        }

        public bool Update(BearProfile item)
        {
            return _repo.Update(item);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        //public IQueryable<BearProfile> Search(string? Name, string? asdas)
        //{
        //    //return _repo.Search(modelName, material);
        //}
    }
}
