using DAL;
using DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TypeService
    {
        private readonly TypeRepo _repo;

        public TypeService(TypeRepo repo)
        {
            _repo = repo;
        }

        public List<BearType> GetAll()
        {
            return _repo.GetAll();
        }

        public BearType? GetById(int id)
        {
            return _repo.GetById(id);
        }
    }
}
