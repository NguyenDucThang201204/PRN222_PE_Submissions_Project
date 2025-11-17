using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repo.Models;
using Service;

namespace PE_PRN232_FA25_LeKienLuong_BE.Controllers
{
    [Route("api/BearProfiles")]
    [ApiController]
    public class BearProfilesController : ControllerBase
    {
        private readonly FA25BearDBContext _context;
        private readonly IBearProfilesService _service;

        public BearProfilesController(IBearProfilesService service)
        {
            _service = service;
        }

        // GET: api/BearProfiles
        [Authorize(Roles = "2")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BearProfile>>> GetBearProfile()
        {
            return await _service.GetBearProfilesAsync();
        }

        // GET: api/BearProfiles/5
        [Authorize(Roles = "2")]
        [HttpGet("{id}")]
        public async Task<ActionResult<BearProfile>> GetBearProfile(int id)
        {
            var bearProfile = await _context.BearProfile.FindAsync(id);

            if (bearProfile == null)
            {
                return NotFound();
            }

            return bearProfile;
        }

        // PUT: api/BearProfiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "2")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBearProfile(int id, BearProfile bearProfile)
        {
            if (id != bearProfile.BearProfileId)
            {
                return BadRequest();
            }

            _context.Entry(bearProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BearProfileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BearProfiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "2")]
        [HttpPost]
        public async Task<ActionResult<BearProfile>> PostBearProfile(BearProfile bearProfile)
        {
            _context.BearProfile.Add(bearProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBearProfile", new { id = bearProfile.BearProfileId }, bearProfile);
        }

        // DELETE: api/BearProfiles/5
        [Authorize(Roles = "2")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBearProfile(int id)
        {
            var bearProfile = await _context.BearProfile.FindAsync(id);
            if (bearProfile == null)
            {
                return NotFound();
            }

            _context.BearProfile.Remove(bearProfile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BearProfileExists(int id)
        {
            return _context.BearProfile.Any(e => e.BearProfileId == id);
        }
    }
}
