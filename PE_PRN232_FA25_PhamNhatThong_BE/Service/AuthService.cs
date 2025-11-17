using Repository;
using Repository.Models;
using Service.Dtos;
using Service.Security;

namespace Service
{
    public class AuthService(AccountRepository _repo, JwtHelper _jwtHelper)
    {
        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorCode = "HB40001",
                    ErrorMessage = "Email and password are required"
                };
            }
            var user = await _repo.Login(email, password);
            if (user == null)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorCode = "HB40101",
                    ErrorMessage = "Invalid email or password"
                };
            }
            string? roleName = null;
            if (user.RoleId != 0 && Enum.IsDefined(typeof(RoleEnum), user.RoleId))
            {
                roleName = ((RoleEnum)user.RoleId).GetRoleName();
            }
            if (!_jwtHelper.IsAllowedRole(roleName))
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorCode = "HB40301",
                    ErrorMessage = "User role is not allowed"
                };
            }
            var token = _jwtHelper.GenerateJwtToken(user, roleName!);
            return new AuthResult
            {
                Success = true,
                Token = token,
                //Role = roleName
                Role = user.RoleId.ToString()
            };
        }
    }
}
