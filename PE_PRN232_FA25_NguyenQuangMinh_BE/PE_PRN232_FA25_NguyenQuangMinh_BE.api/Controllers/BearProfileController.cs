using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.ModelExtensions;
using PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.Models;
using PE_PRN232_FA25_NguyenQuangMinh_BE.Service;

namespace PE_PRN232_FA25_NguyenQuangMinh_BE.api.Controllers
{
    [Authorize]
    [Route("api/BearProfiles")]
    [ApiController]
    public class BearProfileController : ControllerBase
    {
        private readonly IBearProfileService _service;

        public BearProfileController(IBearProfileService service)
        {
            _service = service;
        }
        // GET: api/
        [EnableQuery]
        [Authorize(Roles = "1,2")]
        [HttpGet]
        public async Task<IEnumerable<BearProfile>> Get()
        {
            return await _service.GetAllAsync();
        }

        // GET api/
        [Authorize(Roles = "1,2")]
        [HttpGet("{id}")]
        public async Task<BearProfile> Get(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        // POST api/
        [Authorize(Roles = "1,2")]
        [HttpPost]
        public async Task<int> Post(BearProfile bearProfile)
        {
            return await _service.CreateAsync(bearProfile);
        }

        // PUT api/
        [Authorize(Roles = "1,2")]
        [HttpPut]
        public async Task<int> Put(BearProfile bearProfile)
        {
            return await _service.UpdateAsync(bearProfile);
        }

        // DELETE api/
        [Authorize(Roles = "1,2")]
        [HttpDelete("{id}")]
        public async Task<Boolean> Delete(int id)
        {
            return await _service.DeleteAsync(id);
        }

        [Authorize(Roles = "1,2,3,4")]
        [HttpGet("Search")]
        public async Task<IEnumerable<BearProfile>> Search(string? BearName, string? BearWeight, string? BearTypeName)
        {
            return await _service.SearchAsync(BearName, BearWeight, BearTypeName);
        }

        //[Authorize(Roles = "1, 2")]
        //[HttpPost("SearchWithPaging")]
        //public async Task<PaginationResult<List<VehiclesDatPht>>> SearchWithPaging(VehiclesDatPhtSearchRequest searchRequest)
        //{
        //    return await _service.SearchWithPaginationAsync(searchRequest);
        //}
    }
}
