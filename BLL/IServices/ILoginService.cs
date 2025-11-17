using DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface ILoginService
    {
        Task<LoginResponseDTO> LoginFunc(string email, string password);
    }
}
