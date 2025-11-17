using BearPetManagement_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BearPetManagement_API.Controllers
{
    [Route("BearProfiles")]
    [ApiController]
    public class BearsController : ControllerBase
    {
        private readonly BearService _service;
        public BearsController(BearService service)
        {
            _service = service;
        }

        // GET: api/<BearsController>
        [Authorize(Roles = "1, 2, 3, 4")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var bears = await _service.GetAll();

            return Ok(bears);
        }

        // GET api/<BearsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BearsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BearsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BearsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
