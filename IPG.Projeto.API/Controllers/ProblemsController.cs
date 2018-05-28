using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPG.Projeto.API.Data;
using IPG.Projeto.API.Models;
using IPG.Projeto.API.Models.Stats;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Globalization;

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


        //// GET: api/Problems/stats/council/5
        [HttpGet("stats/council/{id}")]
        //[Authorize("Bearer")]
        public async Task<IActionResult> GetStatsCouncilProblem([FromRoute] int id) 
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // REVER !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!..>:>:>::>::>:>:>:>:>:
            var statsReported = new List<Stat>();
            for (int i = 0; i <= 5; i++)
            { // REVER REVER REVER
                Stat Stat = new Stat();
                var Month = DateTime.Now.AddMonths(-i).Month;
                Stat.Value = await _context.Problems.Where(e => e.CouncilID == id && Month == e.ReportDate.Month).CountAsync();
                //Stat.ValueLabel = Stat.Value.ToString();
                //Stat.Label = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Month);

                statsReported.Add(Stat);
            }
            var statsFixed = new List<Stat>();
            for (int i = 0; i <= 5; i++)
            { // REVER REVER REVER
                Stat Stat = new Stat();
                var Month = DateTime.Now.AddMonths(-i).Month;
                Stat.Value = await _context.Problems.Where(e => e.CouncilID == id && Month == e.LastUpdate.Month && e.State == 3).CountAsync();
                //Stat.ValueLabel = Stat.Value.ToString();
                Stat.Label = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Month);

                statsFixed.Add(Stat);
            }
            var Stats = new List<List<Stat>>
            {
                statsReported,
                statsFixed
            };



            if (Stats == null)
            {
                return NotFound();
            }

            return Ok(Stats);
        }




        //// GET: api/Problems/stats/user/daniel@site.com
        [HttpGet("stats/user/{id}")]
        //[Authorize("Bearer")]
        public async Task<IActionResult> GetStatsUserProblem([FromRoute] string id)    
        {
            
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // REVER !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!..>:>:>::>::>:>:>:>:>:
            var statsReported = new List<Stat>();       
            for (int i = 0; i <=5; i++)
            { // REVER REVER REVER
                Stat Stat = new Stat();
                var Month = DateTime.Now.AddMonths(-i).Month;
                Stat.Value = await _context.Problems.Where(e => e.User.UserName == id && Month == e.ReportDate.Month).CountAsync();
                //Stat.ValueLabel = Stat.Value.ToString();
                Stat.Label = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Month);

                statsReported.Add(Stat);
            }
            var statsFixed = new List<Stat>();  
            for (int i = 0; i <= 5; i++)
            { // REVER REVER REVER
                Stat Stat = new Stat();
                var Month = DateTime.Now.AddMonths(-i).Month;
                Stat.Value = await _context.Problems.Where(e => e.User.UserName == id && Month == e.LastUpdate.Month && e.State == 3).CountAsync();
                //Stat.ValueLabel = Stat.Value.ToString();
                Stat.Label = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Month);

                statsFixed.Add(Stat);
            }
            var Stats = new List<List<Stat>>
            {
                statsReported,
                statsFixed
            };



            if (Stats == null)
            {
                return NotFound();
            }

            return Ok(Stats);
        }


        // GET: api/Problems/Councils/5
        [HttpGet("Council/{ids}")]
        public async Task<IActionResult> GetProblem_byCoucil([FromRoute] string ids)    
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Ids = ids.Split(',').Select(i => Int32.Parse(i)).ToArray(); // separar os council do array  
            var query = _context.Problems.Include("Council").Include("User").Where(p => p.Public == true &&
                                                                                        p.Flagged == false &&
                                                                                        p.Deleted == false)
                                                                                        .AsQueryable();

            var problems = await query.Where(e => Ids.Contains(e.CouncilID)).ToListAsync();  
            if (problems == null)
            {
                return NotFound();
            }

            return Ok(problems);
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