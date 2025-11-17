namespace Service
{
    public interface IJWTService
    {
        string GenerateToken(string name, string email, int? role);
    }
}
