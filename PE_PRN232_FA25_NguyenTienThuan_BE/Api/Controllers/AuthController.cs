using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Api.Response;
using Api.Request;

namespace Api.Controllers
{
    [Route("Accounts/Login")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private BearAccountService bearAccountService;

        private JwtUtil jwtUtil;

        public AuthController(BearAccountService bearAccountService, JwtUtil jwtUtil)
        {
            this.bearAccountService = bearAccountService;
            this.jwtUtil = jwtUtil;
        }

        [HttpPost]
        public async Task<ActionResult<AuthReponse>> Login([FromBody] AuthRequest authRequest)
        {
            try
            {
                var account = await bearAccountService.GetAccountByEmailAndPassword(authRequest.UserName, authRequest.Password);

                if (account != null)
                {
                    string token = jwtUtil.GenerateAccessToken(new JwtPayload(
                            account.AccountId, account.Email, (int)account.RoleId
                        ));
                    var response = new AuthReponse
                    {
                        Token = token,
                        Role = account.RoleId.ToString()
                    };

                    return Ok(response);
                }
                return Unauthorized();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
