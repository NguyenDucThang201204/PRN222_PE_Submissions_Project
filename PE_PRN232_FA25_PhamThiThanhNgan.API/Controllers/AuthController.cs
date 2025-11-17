using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_PhamThiThanhNgan.API.Commons;
using Practice_FA25_PE_Services.DTO;
using Practice_FA25_PE_Services.Interface;


namespace PE_PRN232_FA25_PhamThiThanhNgan.API.Controllers
{
    [AllowAnonymous]
    [ServiceFilter(typeof(ValidateModelStateFilter))]
    [Route("Accounts")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IBearAccountService _accountService;
        
        public AuthController(IBearAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginDto)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(loginDto?.UserName) || string.IsNullOrWhiteSpace(loginDto?.Password))
            {
                return BadRequest(ErrorResponse.InvalidInput("Missing/invalid input"));
            }

            var response = await _accountService.Login(loginDto);

            // Check if account is null
            if (response == null)
            {
                return Unauthorized(ErrorResponse.TokenInvalid("Token missing/invalid"));
            }

            // Check if role is valid (only allow roles 4-7)
            if (!int.TryParse(response.Role, out int roleId) || roleId < 1 || roleId > 4)
            {
                return StatusCode(403, ErrorResponse.PermissionDenied("Permission denied"));
            }

            return Ok(response);
        }
    }
}
