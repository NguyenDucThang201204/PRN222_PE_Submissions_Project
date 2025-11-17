using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebAPI.Controllers
{
    public class BearController
    {
        //[ApiController]
        //public class AuthController : ControllerBase
        //{
        //    private readonly BearService _service;

        //    public AuthController(BearService bearService)
        //    {
        //        _service = bearService;
        //    }

        //    [HttpGet("BearProfiles")]
        //    public async Task<IActionResult> Login(LoginRequest request)
        //    {
        //        try
        //        {
        //            var response = await _service.Authenticate(request);
        //            if (response == null)
        //            {
        //                return Unauthorized();
        //            }
        //            return Ok(response);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message);
        //        }
        //    }
        //}
    }
}
