namespace PE_PRN232_FA25_NguyenThiMaiTrinh_BE
{
    public class LoginRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public string Role { get; set; }

    }
}
