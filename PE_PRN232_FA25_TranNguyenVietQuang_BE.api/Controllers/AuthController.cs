using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_TranNguyenVietQuang_BE_Service.Interfaces;
using PE_PRN232_FA25_TranNguyenVietQuang_BE_api.Services;
using PE_PRN232_FA25_TranNguyenVietQuang_BE_api.Models;

namespace PE_PRN232_FA25_TranNguyenVietQuang_BE_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IBearAccountService _userService;
    private readonly IJwtService _jwtService;

    public AuthController(IBearAccountService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new ErrorResponse
            {
                ErrorCode = "HB40001",
                Message = "UserName and Password are required"
            });
        }

        var user = await _userService.GetByUserNameAsync(request.Email);
        
        if (user == null || user.Password != request.Password)
        {
            return Unauthorized(new ErrorResponse
            {
                ErrorCode = "HB40101",
                Message = "Invalid credentials"
            });
        }


        var token = _jwtService.GenerateToken(request.Email, user.RoleId.ToString());

        return Ok(new LoginResponse
        {
            Token = token,
            Role = user.RoleId.ToString()
        });
    }
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}