using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPG.Projeto.API.Data;
using IPG.Projeto.API.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IPG.Projeto.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Problems")]
    public class ProblemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProblemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Problems
        [HttpGet]
        public IEnumerable<Problem> GetProblems()
        {
            return _context.Problems;
        }

        //// GET: api/Problems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProblem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var problem = await _context.Problems.SingleOrDefaultAsync(e => e.Id == id);

            if (problem == null)
            {
                return NotFound();
            }

            return Ok(problem);
        }

        // GET: api/Problems/5

        [HttpGet("Council/{id}")]
        public async Task<IActionResult> GetProblem_byCoucil([FromRoute] string id)    
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ids = id.Split(',').Select(i => Int32.Parse(i)).ToArray(); // separar os council do array
            int cenas = ids[0];

            var problems = await GetAllCouncils().Where(e => ids.Contains(e.CouncilID)).ToListAsync();  
            

            if (problems == null)
            {
                return NotFound();
            }

            return Ok(problems);
        }
        // async query cenas .. melhor alternativa até agora
        public IQueryable<Problem> GetAllCouncils()
        {

            return _context.Problems.Include("Council").AsQueryable();    

            //return _context.Problems.AsQueryable();


        }

        // PUT: api/Problems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProblem([FromRoute] int id, [FromBody] Problem problem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != problem.Id)
            {
                return BadRequest();
            }

            _context.Entry(problem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProblemExists(id))
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

        // POST: api/Problems
        [HttpPost]
        [Authorize("Bearer")]
        public async Task<IActionResult> PostProblem([FromBody] Problem problem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Problem Problem = problem;  // default construtor
            problem.ApplicationUserID = User.FindFirst("ApplicationUserID").Value;

            _context.Problems.Add(problem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProblem", new { id = problem.Id }, problem);
        }

        // DELETE: api/Problems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProblem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var problem = await _context.Problems.SingleOrDefaultAsync(m => m.Id == id);
            if (problem == null)
            {
                return NotFound();
            }

            _context.Problems.Remove(problem);
            await _context.SaveChangesAsync();

            return Ok(problem);
        }

        private bool ProblemExists(int id)
        {
            return _context.Problems.Any(e => e.Id == id);
        }
    }
}