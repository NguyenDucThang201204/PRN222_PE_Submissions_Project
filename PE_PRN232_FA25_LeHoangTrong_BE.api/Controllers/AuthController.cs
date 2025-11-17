using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_LeHoangTrong_BE_api.Models;
using PE_PRN232_FA25_LeHoangTrong_BE_api.Services;
using PE_PRN232_FA25_LeHoangTrong_BE_service.Interfaces;

namespace PE_PRN232_FA25_LeHoangTrong_BE_api.Controllers;

[ApiController]
[Route("/")]
public class AuthController : ControllerBase
{
    private readonly IBearAccountService _userService;
    private readonly IJwtService _jwtService;

    public AuthController(IBearAccountService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost("/Accounts/Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new ErrorResponse
            {
                ErrorCode = "400",
                Message = "Email and Password are required"
            });
        }

        var user = await _userService.GetByEmailAsync(request.UserName);

        if (user == null || user.Password.Trim() != request.Password)
        {
            return Unauthorized(new ErrorResponse
            {
                ErrorCode = "400",
                Message = "Invalid credentials"
            });
        }

        var roleValue = user.RoleId;
        var role = roleValue == null ? null : roleValue.ToString();
        var token = "";
        if (roleValue != null)
        {
            switch (roleValue)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    token = _jwtService.GenerateToken(request.UserName, role);
                    break;
                default:
                    break;
            }
        }
        else
        {
            return Unauthorized(new ErrorResponse
            {
                ErrorCode = "403",
                Message = "Role not authorized"
            });
        }

        return Ok(new LoginResponse
        {
            Token = token,
            Role = role
        });
    }
}

public class LoginRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}