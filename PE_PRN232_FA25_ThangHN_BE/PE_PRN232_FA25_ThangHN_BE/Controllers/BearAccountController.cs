using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

namespace PE_PRN232_FA25_ThangHN_BE.Controllers
{
    [ApiController]
    [Route("Accounts")]
    public class BearAccountController : ControllerBase
    {
        private readonly BearAccountService _service;
        private readonly IConfiguration _configuration;
        public BearAccountController(BearAccountService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var account = await _service.Authenticate(request.userName, request.password);

            if (account == null)
                throw new AuthenticationException("Invalid Email or Password");

            var result = _service.GenerateJWTToken(account, _configuration);

            return Ok(new LoginResponse(Token:result.token));
        }
    }
    public sealed record LoginRequest
    (
         [Required(ErrorMessage = "Email is required")]
         [EmailAddress(ErrorMessage = "Invalid email format")]
         string userName,

         [Required(ErrorMessage = "Password is required")]
         string password
    );
    public sealed record LoginResponse
    (
        string Token
    );
}
