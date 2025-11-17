using Api.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Service;

namespace Api.Controllers
{
    [Route("BearProfiles")]
    [ApiController]
    //[Authorize]
    public class BearProfileController : ControllerBase
    {
        private BearProfileService bearProfileService;

        public BearProfileController(BearProfileService bearProfileService)
        {
            this.bearProfileService = bearProfileService;
        }

        [HttpGet]
        public async Task<ActionResult<BearProfile>> GetAll()
        {
            var bears = await bearProfileService.GetAll();
            return Ok(bears);
        }

        [HttpPost]
        public async Task<ActionResult<BearProfile>> Create([FromBody]BearProfileReqest bearProfileReqest)
        {
            var bear = new BearProfile()
            {
                BearProfileId = bearProfileReqest.BearProfileId,
                BearName = bearProfileReqest.BearName,
                BearTypeId = bearProfileReqest.BearTypeId,
                BearWeight = bearProfileReqest.BearWeight,
                Characteristics = bearProfileReqest.Characteristics,
                CareNeeds = bearProfileReqest.CareNeeds
            };
            var result = await bearProfileService.CreateBear(bear);
            return Created();
        }

        [HttpPut]
        public async Task<ActionResult<BearProfile>> Update(
            [FromBody] BearProfileReqest bearProfileReqest
          )
        {
            var bear = await this.bearProfileService.GetById(bearProfileReqest.BearProfileId);

            if (bear == null)
            {
                return NotFound();
            }

            bear.BearProfileId = bearProfileReqest.BearProfileId;
            bear.BearName = bearProfileReqest.BearName;
            bear.BearTypeId = bearProfileReqest.BearTypeId;
            bear.BearWeight = bearProfileReqest.BearWeight;
            bear.Characteristics = bearProfileReqest.Characteristics;
            bear.CareNeeds = bearProfileReqest.CareNeeds;

            var result = await bearProfileService.UpdateBear(bear);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BearProfile>> Delete(
            int id
          )
        {
            var bear = await this.bearProfileService.GetById(id);

            if (bear == null)
            {
                return NotFound();
            }

            var result = await bearProfileService.DeleteBear(bear);
            return Ok(result);
        }
    }
}
