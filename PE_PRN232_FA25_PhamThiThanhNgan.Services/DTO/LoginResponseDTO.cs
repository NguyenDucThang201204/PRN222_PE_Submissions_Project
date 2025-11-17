using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_FA25_PE_Services.DTO
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
