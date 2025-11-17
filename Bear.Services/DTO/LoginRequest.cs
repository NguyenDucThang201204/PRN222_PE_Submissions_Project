using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bear.Services.DTO
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string userName { get; set; } = string.Empty;

        [Required]
        public string password { get; set; } = string.Empty ;
    }
}
