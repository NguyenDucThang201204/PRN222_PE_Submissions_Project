using PE_PRN232_FA25_NguyenThaiDuy_BE.Repositories;
using PE_PRN232_FA25_NguyenThaiDuy_BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenThaiDuy_BE.services
{
    public class BearProfileSerivce : IBearProfileService
    {
        private readonly IBearProfileRepository _repository;

        public BearProfileSerivce(IBearProfileRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<BearProfile>> GetAllAsync() => _repository.GetAllAsync();

        public Task<BearProfile?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task<BearProfile> CreateAsync(BearProfileDto dto) => _repository.CreateAsync(dto);

        public Task<bool> UpdateAsync(int id, BearProfileDto dto) => _repository.UpdateAsync(id, dto);

        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);

        public Task<IEnumerable<BearProfile>> SearchAsync(string? bearName, double? bearWeight)
            => _repository.SearchAsync(bearName, bearWeight);
    }
}

