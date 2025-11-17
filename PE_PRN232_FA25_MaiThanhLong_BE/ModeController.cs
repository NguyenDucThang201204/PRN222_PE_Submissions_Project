using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository.Models;
using Service;

namespace PE_PRN232_FA25_MaiThanhLong_BE
{
    [Route("/BearProfiles")]
    [ApiController]
    public class ModelController : Controller
    {
        private readonly ModelService _modelService;

        //constructor
        public ModelController(ModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet]
        [Authorize(Roles = "2")]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _modelService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { errorCode = "500", message = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var result = await _modelService.GetByIdAsync(id);
                if (result == null)
                    return NotFound(new { errorCode = "404", message = "Resource not found" });
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { errorCode = "500", message = "Internal server error" });
            }
        }


        [Authorize(Roles = "2")]
        [HttpPost]

        public async Task<IActionResult> Create([FromBody] RequestModel request)
        {
            try
            {
                var requestModel = new BearProfile
                {
                    BearProfileId = request.BearProfileId,
                    BearTypeId = request.BearTypeId,
                    BearName = request.BearName,
                    BearWeight = request.Weight,
                    Characteristics = request.Characteristics,
                    CareNeeds = request.Careneeds,
                    ModifiedDate = request.ModifiedDate,
                    BearType = null

                };
                var result = await _modelService.CreateAsync(requestModel);
                if (!result)
                {

                    return BadRequest(new { errorCode = "500", message = "Create failed" });
                }
                return Ok(true);
            }
            catch (Exception)
            {
                return StatusCode(500, new { errorCode = "500", message = "Internal server error" });
            }
        }

        //TODO: Get date

        [Authorize(Roles = "2")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] RequestModel request)
        {
            try
            {
                var existingModel = await _modelService.GetByIdAsync(request.BearProfileId);
                if (existingModel == null)
                {
                    return NotFound(new { errorCode = "404", message = "Resource not found" });
                }
                existingModel.BearProfileId = request.BearProfileId;
                existingModel.BearTypeId = request.BearTypeId;
                existingModel.BearName = request.BearName;
                existingModel.BearWeight = request.Weight;
                existingModel.Characteristics = request.Characteristics;
                existingModel.CareNeeds = request.Careneeds;
                existingModel.ModifiedDate = request.ModifiedDate;

                var result = await _modelService.UpdateAsync(existingModel);
                if (!result)
                {
                    return BadRequest(new { errorCode = "400", message = "Update failed" });
                }
                return Ok(true);
            }
            catch (Exception)
            {
                return StatusCode(500, new { errorCode = "500", message = "Internal server error" });
            }
        }

        [Authorize(Roles = "2")]
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _modelService.DeletetAsync(id);
                if (!result)
                {
                    return NotFound(new { errorCode = "404", message = "Resource not found" });
                }
                return Ok(true);
            }
            catch (Exception)
            {
                return StatusCode(500, new { errorCode = "500", message = "Internal server error" });
            }
        }

        //search ODATA
        [Authorize(Roles = "1,2,3,4")]
        [EnableQuery]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            var results = _modelService.GetAllAsync().Result.AsQueryable();
            return Ok(results);
        }

    }
    public class RequestModel
    {
        public int BearProfileId { get; set; }
        public int BearTypeId { get; set; } = 0;
        public string BearName { get; set; } = null!;

        public double Weight { get; set; }

        public string Characteristics { get; set; } = ""!;
        public string Careneeds { get; set; } = ""!;
        public DateTime ModifiedDate { get; set; }
    }
}
