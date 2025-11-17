using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_LeCongHung_BLL.DTOs;
using PE_PRN232_FA25_LeCongHung_BLL.Services;

namespace PE_PRN232_FA25_LeCongHung_BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IAccountService accountService, ILogger<AccountsController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        // POST /Accounts/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _accountService.LoginAsync(request);
                if (result == null)
                {
                    return Unauthorized("Invalid email or password.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

