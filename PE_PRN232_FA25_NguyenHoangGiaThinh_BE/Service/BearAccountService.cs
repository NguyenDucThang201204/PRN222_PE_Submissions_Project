using BO.Dto;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public  class BearAccountService : IBearAccountService
    {
        private readonly IBearAccountRepository _repo;
        private readonly IJwtService _jwtService;

        public BearAccountService(IBearAccountRepository repo, IJwtService jwtservice)
        {
            _repo = repo;
            _jwtService = jwtservice;
        }

        public LoginResponse? Login(LoginRequest request)
        {
            var account = _repo.GetBearAccount(request.userName, request.Password);
            if (account == null)
            {
                return null;
            }
            string roleName = account.RoleId switch
            {
                1 => "Admin",
                2 => "Manager",
                3 => "Staff",
                4 => "member",
            };

            var token = _jwtService.GenerateToken(
           account.AccountId.ToString(),
           account.UserName,
           new List<string> { roleName }
       );

            return new LoginResponse
            {
                Token = token,
                Role = roleName,
            };
        }
    }
}
