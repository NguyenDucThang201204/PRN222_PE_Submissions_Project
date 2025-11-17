using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.ThienNH.ModelExtensions;
using Repositories.ThienNH.Models;
using Services.ThienNH;

namespace WebAPI.ThienNH.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BearProfilesController : Controller
    {
        private readonly IBearProfileService _service;

        public BearProfilesController(IBearProfileService service)
        {
            _service = service;
        }
    

        [Authorize(Roles = "2")]
        [HttpGet]
        public async Task<IEnumerable<BearProfile>> Get()
        {
            return await _service.GetAllAsync();
        }

     
        [Authorize(Roles = "2")]
        [HttpGet("{id}")]
        public async Task<BearProfile> Get(int id)
        {
            return await _service.GetByIdAsync(id);
        }

    
        [Authorize(Roles = "2")]
        [HttpPost]
        public async Task<int> Post(BearProfile bearProfile)
        {
            return await _service.CreateAsync(bearProfile);
        }

        [Authorize(Roles = "2")]
        [HttpPut]
        public async Task<int> Put(BearProfile bearProfile)
        {
            return await _service.UpdateAsync(bearProfile);
        }

  
        [Authorize(Roles = "2")]
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _service.DeleteAsync(id);
        }

        [Authorize(Roles = "2,3,4")]
        [HttpPost("Search")]
        public async Task<PaginationResult<List<BearProfile>>> SearchWithPagingAsync(BearProfileSearchRequest searchRequest)
        {
            return await _service.SearchWithPagingAsync(searchRequest);
        }
    }
}
