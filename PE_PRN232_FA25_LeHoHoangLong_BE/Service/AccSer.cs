using Repository;
using Repository.DTOs;

namespace Service
{
    public class AccSer : IAccSer
    {
        private readonly IAccRepo _accRepo;
        private readonly IJWTSer _jwtSer;
        public AccSer(IAccRepo accRepo, IJWTSer jwtSer)
        {
            _accRepo = accRepo;
            _jwtSer = jwtSer;
        }
        public async Task<LoginResponse?> LoginAsync(LoginRequest re)
        {
            var account = await _accRepo.LoginAsync(re.UserName, re.Password);
            if (account == null)
            {
                return null;
            }

            string token = _jwtSer.GenerateToken(account.UserName, account.Email, account.RoleId);

            return new LoginResponse
            {
                Token = token,
                Role = account.RoleId,
            };
        }

    }
}
