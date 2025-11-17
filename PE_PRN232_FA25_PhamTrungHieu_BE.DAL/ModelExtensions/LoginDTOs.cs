namespace PE_PRN232_FA25_PhamTrungHieu_BE.DAL.ModelExtensions
{
    public record LoginRequest(string Email, string Password);
    public class LoginResponse
    {
        public string Token {  get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;    
    }
}
