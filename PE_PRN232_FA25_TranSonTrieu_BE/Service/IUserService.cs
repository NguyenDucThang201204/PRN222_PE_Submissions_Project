using DAO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        public Task<LoginResponse> Login(LoginRequest loginDto);
    }
}
