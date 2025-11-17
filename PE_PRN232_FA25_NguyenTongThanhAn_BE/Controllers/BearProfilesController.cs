using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Enums;

namespace PE_PRN232_FA25_NguyenTongThanhAn_BE.Controllers
{
    [ApiController]
    [Route("BearProfiles")]
    [Authorize]
    public class BearProfilesController : Controller
    {
        private readonly BearProfileService bearProfileService;
        public BearProfilesController(BearProfileService bearProfileService)
        {
            this.bearProfileService = bearProfileService;
        }
        [HttpGet]
        [Authorize(Roles = RoleName.AllRoles)]
        public async Task<IActionResult> GetAll()
        {
            var result = await bearProfileService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        [Authorize(Roles = RoleName.AllRoles)]
        public async Task<IActionResult> GetById(int id)
        {
            var bag = await bearProfileService.GetByIdAsync(id);
            if (bag == null)
                return NotFound(new { message = "BearProfile not found" });

            return Ok(bag);
        }
    }
}
