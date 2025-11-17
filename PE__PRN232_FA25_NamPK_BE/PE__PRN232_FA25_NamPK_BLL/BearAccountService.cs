using PE__PRN232_FA25_NamPK_BLL.DTO;
using PE__PRN232_FA25_NamPK_DAL;
using PE__PRN232_FA25_NamPK_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE__PRN232_FA25_NamPK_BLL
{
    public class BearAccountService
    {
        private readonly BearAccountRepository _repository;

        public BearAccountService(BearAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<BearAccount> Authenticate(AuthRequest request)
        {
            // login Username = Email
            string email = request.UserName;
            string password = request.Password;

            var existAccount = await _repository.Authenticate(email, password);

            if (existAccount == null)
            {
                return null;
            }
            return existAccount;
        }
    }
}
