using DAO.DTO;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly UserDAO _dao;
        public UserService(UserDAO dao)
        {
            _dao = dao;
        }

        public Task<LoginResponse> Login(LoginRequest loginDto)
        {
            if (loginDto == null)
                throw new ArgumentNullException(nameof(loginDto));

            return _dao.Login(loginDto);
        }

    }
}
