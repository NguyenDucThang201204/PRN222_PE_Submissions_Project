using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Service;
using Service.Model;

namespace Presentation.Controllers
{
    [Route("BearProfiles")]
    [ApiController]
    public class BearProfilesController : ODataController
    {
        private readonly BearService _bearService;
        public BearProfilesController(BearService bearService)
        {        
            _bearService = bearService;
        }

        [Authorize(Roles = "1,3,4")]
        [HttpGet]
       public async Task<IActionResult> GetAll()
        {
            var handbags = await _bearService.GetAllBearAsync();
            return Ok(handbags);
        }
        [Authorize(Roles = "1,3,4")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var handbag = await _bearService.GetBearByIdAsync(id);
            if (handbag == null)
            {
                return NotFound(new { errorCode = "HB40401", message = "Resource not found" });
            }
            return Ok(handbag);
        }
        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BearCreateDTO handbag)
        {
            try
            {
                await _bearService.AddBearAsync(handbag);
                return Ok("Add succesfully");
            }
            catch (ArgumentNullException ex)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest();
            }
        }
        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BearCreateDTO handbag)
        {
            try
            {
                await _bearService.UpdateBearAsync(id, handbag);
                return Ok("Update suceesfully");
            }
            catch (ArgumentNullException ex)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest();
            }
            
        }
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _bearService.DeleteBearAsync(id);
                return Ok("Delete success");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest();
            }   
        }
        [Authorize(Roles = "1,3,4")]
        [EnableQuery]
        [HttpPost("search")]     
        public async Task<IActionResult> Search(PaginationSearch search)
        {
            var result = await _bearService.Search(search);
            return Ok(result);
        }

    }
}
