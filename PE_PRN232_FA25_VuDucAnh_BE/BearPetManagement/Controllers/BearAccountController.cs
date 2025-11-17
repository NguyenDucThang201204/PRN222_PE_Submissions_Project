using BearPetManagement_Repositories.DTOs;
using BearPetManagement_Services;
using Microsoft.AspNetCore.Mvc;

namespace BearPetManagement.Controllers
{
    [ApiController]
    [Route("Accounts")]
    public class BearAccountController : Controller
    {
        private readonly BearAccountService _bearAccountService;

        public BearAccountController(BearAccountService bearAccountService)
        {
            _bearAccountService = bearAccountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                var error = ApiError._errors[400];
                return BadRequest(new ApiError(error.ErrorCode, "Missing/invalid input"));
            }

            try
            {
                var account = await _bearAccountService.GetAccountAsync(loginDTO.userName, loginDTO.Password);
                if (account == null)
                {
                    var error = ApiError._errors[404];
                    return NotFound(new ApiError(error.ErrorCode, "Account not found"));
                }

                var token = await _bearAccountService.GenerateTokenAsync(account);

                var response = new AuthenticationResponse
                {
                    Token = token,
                    Account = account
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = ApiError._errors[500];
                return StatusCode(500, new ApiError(error.ErrorCode, "Internal server error"));
            }
        }
    }
}
