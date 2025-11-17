namespace FA25Bear.Service
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string email, int roles);
    }
}
