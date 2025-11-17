using PE_PRN232_FA25_PhanTuanAn_BE.Repositories;
using PE_PRN232_FA25_PhanTuanAn_BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_PhanTuanAn_BE.Services
{
    public class BearTypeService
    {
        private readonly BearTypeRepository _repository;
        public BearTypeService() => _repository = new BearTypeRepository();
        public async Task<List<BearType>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex) { }
            return new List<BearType>();
        }
    }
}
