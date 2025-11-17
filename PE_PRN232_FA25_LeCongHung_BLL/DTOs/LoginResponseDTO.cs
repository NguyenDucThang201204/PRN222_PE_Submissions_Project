namespace PE_PRN232_FA25_LeCongHung_BLL.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FullName { get; set; }
        public int? RoleId { get; set; }
    }
}

