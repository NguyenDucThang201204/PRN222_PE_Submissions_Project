using Practice_FA25_PE_Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_FA25_PE_Services.Interface
{
    public interface IBearAccountService
    {
        Task<LoginResponseDTO?> Login(LoginRequestDTO loginRequest);
    }
}
