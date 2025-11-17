using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Dto;
using Services;

namespace WebAPI.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AccountService _service;

        public AuthController(AccountService accountService)
        {
            _service = accountService;
        }

        [HttpPost("Accounts/Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var response = await _service.Authenticate(request);
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
