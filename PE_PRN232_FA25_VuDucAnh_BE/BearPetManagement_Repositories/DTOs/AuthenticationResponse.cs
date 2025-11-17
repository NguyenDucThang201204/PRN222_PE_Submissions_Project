using BearPetManagement_Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearPetManagement_Repositories.DTOs
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public BearAccount Account { get; set; }
    }
}
