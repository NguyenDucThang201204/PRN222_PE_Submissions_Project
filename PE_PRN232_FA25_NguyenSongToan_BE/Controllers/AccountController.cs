using Microsoft.AspNetCore.Mvc;
using Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

namespace PE_PRN232_FA25_NguyenSongToan_BE.Controllers
{
    public sealed record LoginRequest
      (
           [Required(ErrorMessage = "Email is required")]
             [EmailAddress(ErrorMessage = "Invalid email format")]
             string Email,

           [Required(ErrorMessage = "Password is required")]
             string Password
      );

    public sealed record LoginResponse
    (
        string Token,
        string Role
    );


    [Route("Accounts/Login")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly BearAccountService _service;
        private readonly IConfiguration _configuration;

        public AccountController(BearAccountService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var account = await _service.Authenticate(request.Email, request.Password);

            if (account == null)
                throw new AuthenticationException("Invalid Email or Password");

            var result = _service.GenerateJWTToken(account, _configuration);

            return Ok(new LoginResponse
            (
                Token: result.token,
                Role: result.role
            ));
        }
    }
}
