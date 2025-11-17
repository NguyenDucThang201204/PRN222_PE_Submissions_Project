using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA25Bear.Repositories.DTOs
{
    public class AuthResponse
    {
        public string Token { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
