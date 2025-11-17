
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Requests;


namespace PE_PRN232_FA25_NguyenTongThanhAn_BE.Controllers
{
    [ApiController]
    [Route("Accounts")]
    public class AuthController : Controller
    {
        private readonly BearAccountService _service;
        public AuthController(BearAccountService service)
        {
            _service = service;
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _service.GetUserAccount(request.Email, request.Password);
            if (response == null)
                return Unauthorized(new
                {
                    message = "Invalid username or password"
                });

            return Ok(response);
        }


    }
}
