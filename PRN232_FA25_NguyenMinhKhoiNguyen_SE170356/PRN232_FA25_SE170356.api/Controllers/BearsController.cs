using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PRN232_SU25_NguyenNMK_BusinessLogicLayer.Services;
using PRN232_SU25_NguyenNMK_DataAccessLayer.DTOs;
using System.Reflection.PortableExecutable;

namespace PRN232_FA25_SE170356.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BearsController : Controller
    {
        private readonly IBearProfileService _bearService;

        public BearsController(IBearProfileService bearService)
        {
            _bearService = bearService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager, Staff, Member")]
        public async Task<IActionResult> GetAll()
        {
            var bears = await _bearService.GetAllAsync();
            return Ok(bears);
        }

        [HttpGet("odata")]
        [EnableQuery]
        [Authorize(Roles = "Manager, Staff, Member")]
        public async Task<IActionResult> GetOData([FromQuery] string bearName = null, [FromQuery] string bearTypeName = null)
        {
            var bears = await _bearService.GetAllAsync(bearName, bearTypeName);
            return Ok(bears);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Manager, Staff, Member")]
        public async Task<IActionResult> GetById(int id)
        {
            var bears = await _bearService.GetByIdAsync(id);
            return Ok(bears);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([FromBody] BearCreateUpdateDTO dto)
        {
            var bears = await _bearService.CreateAsync(dto);
            return StatusCode(201, bears);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Update(int id, [FromBody] BearCreateUpdateDTO dto)
        {
            var bears = await _bearService.UpdateAsync(id, dto);
            return Ok(bears);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bearService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("search")]
        [Authorize(Roles = "Manager, Staff, Member")]
        public async Task<IActionResult> Search([FromQuery] string bearName, [FromQuery] string bearTypeName)
        {
            var results = await _bearService.SearchAsync(bearName, bearTypeName);
            return Ok(results);
        }
    }
}
