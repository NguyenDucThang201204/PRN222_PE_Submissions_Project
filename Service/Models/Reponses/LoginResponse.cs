using Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Reponses
{
    public class LoginResponse
    {
        public string Token { get; set; } = default!;

        public string Role { get; set; } = default!;
    }
}
