using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.ModelExtensions;
using Repositories.Models;
using Services;

namespace PE_PRN232_FA25_HoaKhanhHung_BE.Controllers
{
    [Route("[controller]")]
    [ApiController] 
    public class BearProfileController : ControllerBase
    {
        private readonly IBearProfileService _profileService;
        private readonly BearTypeService _typeService;

        public BearProfileController(IBearProfileService profileService, BearTypeService typeService)
        {
            _profileService = profileService;
            _typeService = typeService;
        }
        [HttpGet]
        [Authorize(Roles = "2")]
        public async Task<IEnumerable<BearProfile>> Get()
        {
            return await _profileService.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "2")]
        public async Task<BearProfile> Get(int id)
        {
            return await _profileService.GetById(id);
        }

        [HttpPost]
        [Authorize(Roles = "2")]
        public async Task<int> Post(BearProfile post)
        {
            return await _profileService.Create(post);
        }

        [HttpPut]
        [Authorize(Roles = "2")]
        public async Task<int> Put(BearProfile put)
        {
            return await _profileService.Update(put);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "2")]
        public async Task<bool> Delete(int id)
        {
            return await _profileService.Delete(id);
        }

        [Authorize(Roles = "2, 3, 4")]
        [HttpGet("Search")]
        public async Task<ActionResult<PaginationResult<List<BearProfile>>>> Get([FromQuery] int? weight, [FromQuery] string? bearName, [FromQuery] string? bearTypeName, [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
        {
            return await _profileService.SearchWithPagingAsync(bearName ?? "", weight ?? 0, bearTypeName ?? "", currentPage, pageSize);
            
        }
    }
}
