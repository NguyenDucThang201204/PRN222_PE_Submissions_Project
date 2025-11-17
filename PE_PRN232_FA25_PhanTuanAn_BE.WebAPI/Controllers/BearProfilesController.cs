using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PE_PRN232_FA25_PhanTuanAn_BE.Repositories.Models;
using PE_PRN232_FA25_PhanTuanAn_BE.Services;

namespace PE_PRN232_FA25_PhanTuanAn_BE.WebAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class BearProfilesController : ControllerBase
    {
        private readonly IBearProfileService _service;
        public BearProfilesController(IBearProfileService service) => _service = service;

        // GET: api/<StationsAnPtController>
        [EnableQuery]
        [Authorize(Roles = "1, 2")]
        [HttpGet]
        public async Task<List<BearProfile>> Get()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<StationsAnPtController>/5
        [Authorize(Roles = "1, 2")]
        [HttpGet("{id}")]
        public async Task<BearProfile> Get(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        // POST api/<StationsAnPtController>
        [Authorize(Roles = "1, 2")]
        [HttpPost("Create")]
        public async Task<int> Post(BearProfile bear)
        {
            return await _service.CreateAsync(bear);
        }

        // PUT api/<StationsAnPtController>/5
        [Authorize(Roles = "1, 2")]
        [HttpPut("Update")]
        public async Task<int> Put(int id, BearProfile bear)
        {
            return await _service.UpdateAsync(bear);
        }

        // DELETE api/<StationsAnPtController>/5
        [Authorize(Roles = "1, 2")]
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
