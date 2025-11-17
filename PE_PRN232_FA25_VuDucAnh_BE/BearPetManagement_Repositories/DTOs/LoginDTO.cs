using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearPetManagement_Repositories.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string userName { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}
