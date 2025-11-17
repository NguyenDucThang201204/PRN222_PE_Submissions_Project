using BLL;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PE_PRN232_FA25_VoHoangTuanKiet_BE.Models;
using System.Reflection.PortableExecutable;

namespace PE_PRN232_FA25_VoHoangTuanKiet_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BearProfilesController : ControllerBase
    {
        private readonly ProfileService _profileService;
        private readonly TypeService _typeService;

        public BearProfilesController(TypeService TypeService, ProfileService profileService)
        {
            _typeService = TypeService;
            _profileService = profileService;
        }

        // GET: api/<BearProfileController>
        [HttpGet]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> Get()
        {
            var BearProfiles = _profileService.GetAll();
            return Ok(BearProfiles);
        }

        // GET api/<BearProfileController>/5
        [HttpGet("{id}")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> Get(int id)
        {
            var BearProfiles = _profileService.GetById(id);
            if (BearProfiles == null)
                return NotFound();
            return Ok(BearProfiles);
        }

        // GET: api/BearProfiles/search?modelName=...&material=...
        //[HttpGet("search")]
        //[Authorize(Policy = "user")]
        //[EnableQuery(MaxExpansionDepth = 3, MaxTop = 100)]
        //public IQueryable Search([FromQuery] string? modelName, [FromQuery] string? material)
        //{
        //    var item = _profileService.Search(modelName, material);

        //    return item;

        //}

        // POST api/<BearProfileController>
        [HttpPost]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Post([FromBody] Dto model)
        {
            var item = MapToDto(model);
            item.BearProfileId = 0;

            if (_typeService.GetById(item.BearTypeId) == null)
            {
                return BadRequest(new { errorCode = "400", message = "Invalid BearProfile data" });
            }

            if (!_profileService.Add(item))
            {
                return BadRequest(new { errorCode = "400", message = "Unable to add BearProfile" });
            }

            return Ok();
        }

        // PUT api/<BearProfileController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Put(int id, [FromBody] Dto model)
        {
            var old = _profileService.GetById(id);
            if (old == null)
                return NotFound();

            var item = MapToDto(model);
            item.BearProfileId = id;


            old.BearTypeId = item.BearTypeId;
            old.BearName = item.BearName;
            old.BearProfileId = item.BearProfileId;
            old.BearWeight = item.BearWeight;
            old.CareNeeds = item.CareNeeds;
            old.Characteristics = item.Characteristics;
            old.ModifiedDate = item.ModifiedDate;

            if (_typeService.GetById(item.BearTypeId) == null)
            {
                return BadRequest(new { errorCode = "400", message = "Invalid BearProfile data" });
            }

            if (!_profileService.Update(old))
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE api/<BearProfileController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_profileService.GetById(id) == null)
                return NotFound();

            if (!_profileService.Delete(id))
            {
                return BadRequest();
            }

            return Ok();
        }

        private BearProfile MapToDto(Dto h)
        {
            return new BearProfile
            {
                BearTypeId = h.BearTypeId,
                BearName = h.BearName,
                BearProfileId = h.BearProfileId,
                BearWeight = h.BearWeight,
                CareNeeds = h.CareNeeds,
                Characteristics = h.Characteristics,
                ModifiedDate = h.ModifiedDate
            };
        }
    }
}
