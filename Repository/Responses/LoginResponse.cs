using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Role { get; set; }    
    }
}
