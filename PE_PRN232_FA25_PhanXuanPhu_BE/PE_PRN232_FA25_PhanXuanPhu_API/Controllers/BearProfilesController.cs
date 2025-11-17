using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PE_PRN232_FA25_PhanXuanPhu_API.Models;
using Repositories.ModelExtensions;
using Repositories.Models;
using Services;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PE_PRN232_FA25_PhanXuanPhu_API.Controllers
{
    [Route("BearProfiles")]
    [ApiController]
    public class BearProfilesController : ControllerBase
    {
        private readonly BearProfileService _service;

        public BearProfilesController(BearProfileService service)
        {
            _service = service;
        }

  
        [HttpGet]
        [Authorize(Roles = "2,3,4")]
        public async Task<IEnumerable<BearProfile>> Get()
        {
            return await _service.GetAllAsync();
        }

    
        [HttpGet("{id}")]
        [Authorize(Roles = "2,3,4")]
        public async Task<IActionResult> Get(int id)
        {
            var bear = await _service.GetByIdAsync(id);
            if (bear == null)
            {
                return ErrorHelper.NotFound();
            }
            return Ok(bear);
        }

 
        [HttpPost]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Post(BearProfileRequestForm request)
        {
            var item = new BearProfile
            {
                //BearProfileId = request.BearProfileId,
                BearTypeId = request.BearTypeId,
                BearName = request.BearName,
                BearWeight = request.BearWeight,
                Characteristics = request.Characteristics,
                CareNeeds = request.CareNeeds,
                ModifiedDate = DateTime.Now,
            };
            var newItemId = await _service.CreateAsync(item);

            item.BearProfileId = newItemId;

            return CreatedAtAction(nameof(Get), new { id = newItemId }, item);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Put(int id, [FromBody] BearProfileRequestForm request)
        {
            var item = new BearProfile
            {
                BearProfileId = id,
                BearTypeId = request.BearTypeId,
                BearName = request.BearName,
                BearWeight = request.BearWeight,
                Characteristics = request.Characteristics,
                CareNeeds = request.CareNeeds,
                ModifiedDate = DateTime.Now,
            };

            var result = await _service.UpdateAsync(item);

            if (result == 0)
                return ErrorHelper.NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);

                if (!result)
                {
                    return ErrorHelper.NotFound();
                }

                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return ErrorHelper.NotFound();
            }
            catch (Exception ex)
            {
                return ErrorHelper.InternalServer();
            }
        }

        [HttpGet("Search")]
        [Authorize(Roles = "2,3,4")]
        public async Task<IEnumerable<BearProfile>> Search([FromQuery] string? LeopardName, [FromQuery] double? Weight)
        {
            return await _service.SearchAsync(LeopardName, Weight);
        }

        [HttpPost("SearchWithPaging")]
        [Authorize(Roles = "2,3,4")]
        public async Task<PaginationResult<List<BearProfile>>> SearchWithPaging(BearProfileRequest searchRequest)
        {
            return await _service.SearchWithPagingAsync(searchRequest);
        }

        public class BearProfileRequestForm
        {
            //public int BearProfileId { get; set; }

            [Required(ErrorMessage = "BearTypeId is required")]
            public int BearTypeId { get; set; }

            [Required(ErrorMessage = "BearName is required")]
            public string BearName { get; set; }

            [Required(ErrorMessage = "BearWeight is required")]
            public double BearWeight { get; set; }

            [Required(ErrorMessage = "Characteristics is required")]
            public string Characteristics { get; set; }

            [Required(ErrorMessage = "CareNeeds is required")]
            public string CareNeeds { get; set; }
        }
    }
}
