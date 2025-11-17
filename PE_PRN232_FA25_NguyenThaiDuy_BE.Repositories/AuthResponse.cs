using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenThaiDuy_BE.Repositories
{
    public class AuthResponse
    {
        public string Token { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
