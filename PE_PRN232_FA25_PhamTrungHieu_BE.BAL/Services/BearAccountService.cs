using PE_PRN232_FA25_PhamTrungHieu_BE.DAL.ModelExtensions;
using PE_PRN232_FA25_PhamTrungHieu_BE.DAL.Models;
using PE_PRN232_FA25_PhamTrungHieu_BE.DAL.Repositories;

namespace PE_PRN232_FA25_PhamTrungHieu_BE.BAL.Services
{
    public class BearAccountService : IBearAccountService
    {
        private readonly GenericRepository<BearAccount> _repository;

        public BearAccountService(GenericRepository<BearAccount> repository)
        {
            _repository = repository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var repsonse = new LoginResponse();
            var user = (await _repository.FindWithIncludeAsync(
                predicate: query => query.Email == request.Email && query.Password == request.Password)).FirstOrDefault();

            if (user == null) return repsonse;

            repsonse.Role = user.RoleId.ToString();
            return repsonse;
        }
    }
}
