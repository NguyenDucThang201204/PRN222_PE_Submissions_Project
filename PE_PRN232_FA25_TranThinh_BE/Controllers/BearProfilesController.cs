using FA25Bear.DataAccess.Models;
using FA25Bear.DataAccess.Models.DTOs;
using FA25Bear.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace PE_PRN232_FA25_TranThinh_BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BearProfilesController : Controller
    {
        private readonly IBearProfileService _service;
        public BearProfilesController(IBearProfileService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        [Authorize(Roles = "2")]
        public ObjectResponse<List<BearProfile>> GetAll()
        {
            try
            {
                var bearProfiles = _service.GetAll();
                return new ObjectResponse<List<BearProfile>>("200", "success", bearProfiles);
            }
            catch (Exception ex)
            {
                return new ObjectResponse<List<BearProfile>>("500", ex.Message, null);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "2")]
        public ObjectResponse<BearProfile> GetById(int id)
        {
            try
            {
                var bearProfile = _service.GetById(id);
                return new ObjectResponse<BearProfile>("200", "success", bearProfile);
            }
            catch (Exception ex)
            {
                return new ObjectResponse<BearProfile>("500", ex.Message, null);
            }
        }

        [HttpPost]
        [Authorize(Roles = "2")]
        public ObjectResponse<BearProfileDTO> Create(BearProfileDTO dto)
        {
            try
            {
                _service.Create(dto);
                return new ObjectResponse<BearProfileDTO>("200", "success", dto);
            }
            catch (Exception ex)
            {
                return new ObjectResponse<BearProfileDTO>("500", ex.Message, null);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "2")]
        public ObjectResponse<BearProfile> Update(int id)
        {
            try
            {
                var bp = _service.GetById(id);
                _service.Update(bp);
                return new ObjectResponse<BearProfile>("200", "success", bp);
            }
            catch (Exception ex)
            {
                return new ObjectResponse<BearProfile>("500", ex.Message, null);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "2")]
        public ObjectResponse<bool> Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return new ObjectResponse<bool>("200", "success", true);
            }
            catch (Exception ex)
            {
                return new ObjectResponse<bool>("500", ex.Message, false);
            }
        }

        // OData hỗ trợ: $filter, $select, $orderby, $top, $skip
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "2, 3, 4")]
        public IActionResult Get([FromQuery] string? modelName, [FromQuery] int? weight)
        {
            var handbags = _service.Search(modelName, weight);
            return Ok(handbags);
        }
    }
}
