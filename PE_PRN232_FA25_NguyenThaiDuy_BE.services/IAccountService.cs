using PE_PRN232_FA25_NguyenThaiDuy_BE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenThaiDuy_BE.services
{
    public interface IAccountService
    {
        AuthResponse Authenticate(string email, string password);
    }
}
