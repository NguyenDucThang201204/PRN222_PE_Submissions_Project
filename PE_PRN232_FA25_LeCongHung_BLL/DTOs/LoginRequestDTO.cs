using System.ComponentModel.DataAnnotations;

namespace PE_PRN232_FA25_LeCongHung_BLL.DTOs
{
    public class LoginRequestDTO
    {
        [Required]
        public string UserName { get; set; } = null!; // Email string (as username)

        [Required]
        public string Password { get; set; } = null!;
    }
}

