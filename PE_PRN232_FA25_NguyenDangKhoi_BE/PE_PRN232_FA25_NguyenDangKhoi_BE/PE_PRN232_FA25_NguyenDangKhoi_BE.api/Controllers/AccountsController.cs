using Microsoft.AspNetCore.Mvc;
using Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenDangKhoi.api.Controllers
{
    public sealed record LoginRequest
    (
         [Required(ErrorMessage = "UserName is required")]
         [EmailAddress(ErrorMessage = "Invalid username format")]
         string UserName,

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
    public class AccountsController : ControllerBase
    {
        private readonly AccountsService _service;
        private readonly IConfiguration _configuration;

        public AccountsController(AccountsService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var account = await _service.Authenticate(request.UserName, request.Password);

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
