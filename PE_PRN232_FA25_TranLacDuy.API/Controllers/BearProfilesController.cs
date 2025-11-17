using BO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PE_PRN232_FA25_TranLacDuy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BearProfilesController : ControllerBase
    {
        private readonly BearProfileRepo _repo;
        public BearProfilesController (BearProfileRepo repo)
        {
            _repo = repo;
        }
        [HttpPost("Create")]
        public  async Task<IActionResult> AddBear(BearProfile req) {
            var exit = _repo.GetAllBears();
            var nextId = exit.Any() ? exit.Max(h => h.BearProfileId) + 1 : 1;

            var bearP = new BearProfile
            {
                BearProfileId = nextId,  // Manually set the ID
                BearName = req.BearName,
                BearTypeId = req.BearTypeId,
                Characteristics = req.Characteristics,
                Stock = 0,
                BearWeight = req.BearWeight
            };
            _repo.AddBear(bearP);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHandBag(int id, [FromBody] BearRequest req)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            if (userId == null)
            {
                
                return Unauthorized();
            }

            if (userRole != "1" && userRole != "2")
            {
                
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var exit = _repo.GetBearsById(id);


            // Update existing handbag with new values
            exit.BearName = req.BearName;
            exit.Characteristics = req.Characteristics;
            exit.BearTypeId = req.BearTypeId;
            exit.BearWeight = req.BearWeight;

            try
            {
                _repo.UpdateBear(exit);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while updating the handbag");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHandBag(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            if (userId == null)
            {

                return Unauthorized();
            }

            if (userRole != "1" && userRole != "2")
            {

                return StatusCode(StatusCodes.Status403Forbidden);
            }

            _repo.DeleteBear(id);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHandBags()
        {
        
            var list = _repo.GetAllBears();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHandBagById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            if (userId == null)
            {

                return Unauthorized();
            }

            if (userRole != "1" && userRole != "2")
            {

                return StatusCode(StatusCodes.Status403Forbidden);
            }
            var bear = _repo.GetBearsById(id);
            if (bear == null)
            {
                return NotFound();
            }
           
            return Ok(bear);
        }
        public class BearRequest() {
          public string  BearName { get; set; } 
          public int       BearTypeId {  get; set; }
          public string      Characteristics { get; set; } 
                
         public decimal        BearWeight { get; set; } 
        }

    }

}
