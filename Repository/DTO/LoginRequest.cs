using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class LoginRequest
    {
        public string userName { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
