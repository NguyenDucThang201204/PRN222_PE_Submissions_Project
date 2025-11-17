using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo;
using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.Basic;
using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenQuangMinh_BE.Service
{
    public class BearTypeService
    {
        private readonly GenericRepository<BearType> _repository;

        public BearTypeService() => _repository = new GenericRepository<BearType>();

        public async Task<List<BearType>> GetAllAsync() => await _repository.GetAllAsync();
    }
}
