using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Repository.DTO;
using Repository.Models;

namespace Service
{
    public class BearService
    {
        private readonly IBearProfileRepo _repository;
        public BearService(IBearProfileRepo repository)
        {
            _repository = repository;
        }
        public Task<IEnumerable<BearProfile>> GetAllAsync() => _repository.GetAllAsync();
    }
}
