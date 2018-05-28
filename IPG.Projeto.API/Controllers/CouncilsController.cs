using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPG.Projeto.API.Data;
using IPG.Projeto.API.Models;

namespace IPG.Projeto.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Councils")]
    public class CouncilsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CouncilsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Councils
        [HttpGet]
        public IEnumerable<Council> GetCouncil()
        {
            return _context.Council;
        }

        // GET: api/Councils/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCouncil([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var council = await _context.Council.SingleOrDefaultAsync(m => m.Id == id);

            if (council == null)
            {
                return NotFound();
            }

            return Ok(council);
        }

        // GET: api/Councils/5
        [HttpGet("stats/{id}")]
        public async Task<IActionResult> GetCouncilStats([FromRoute] int id)    
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var council = await _context.Council.SingleOrDefaultAsync(m => m.Id == id && m.Deleted == false);

            if (council == null)
            {
                return NotFound();
            }

            return Ok(council);
        }
        // PUT: api/Councils/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCouncil([FromRoute] int id, [FromBody] Council council)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != council.Id)
            {
                return BadRequest();
            }

            _context.Entry(council).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CouncilExists(id))
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

        // POST: api/Councils
        [HttpPost]
        public async Task<IActionResult> PostCouncil([FromBody] Council council)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Council.Add(council);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCouncil", new { id = council.Id }, council);
        }

        // DELETE: api/Councils/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCouncil([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var council = await _context.Council.SingleOrDefaultAsync(m => m.Id == id);
            if (council == null)
            {
                return NotFound();
            }

            _context.Council.Remove(council);
            await _context.SaveChangesAsync();

            return Ok(council);
        }

        private bool CouncilExists(int id)
        {
            return _context.Council.Any(e => e.Id == id);
        }
    }
}