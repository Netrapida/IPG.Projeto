using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPG.Projeto.MVC.Data;
using IPG.Projeto.MVC.Models;

namespace IPG.Projeto.MVC.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProblemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Problems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Problems.Include(p => p.Council).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Problems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problems
                .Include(p => p.Council)
                .Include(p => p.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (problem == null)
            {
                return NotFound();
            }

            return View(problem);
        }

        // GET: Problems/Create
        public IActionResult Create()
        {
            ViewData["CouncilID"] = new SelectList(_context.Council, "CouncilID", "CouncilID");
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            return View();
        }

        // POST: Problems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Latitude,Longitude,Text,Detail,Photo,Anonymous,ReportDate,LastUpdate,WhenSentDate,Flagged,SendFailCount,SenDFailReason,SendFailDate,Counter,Public,State,Deleted,ApplicationUserID,CouncilID")] Problem problem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(problem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CouncilID"] = new SelectList(_context.Council, "CouncilID", "CouncilID", problem.CouncilID);
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUser, "Id", "Id", problem.ApplicationUserID);
            return View(problem);
        }

        // GET: Problems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problems.SingleOrDefaultAsync(m => m.Id == id);
            if (problem == null)
            {
                return NotFound();
            }
            ViewData["CouncilID"] = new SelectList(_context.Council, "CouncilID", "CouncilID", problem.CouncilID);
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUser, "Id", "Id", problem.ApplicationUserID);
            return View(problem);
        }

        // POST: Problems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Latitude,Longitude,Text,Detail,Photo,Anonymous,ReportDate,LastUpdate,WhenSentDate,Flagged,SendFailCount,SenDFailReason,SendFailDate,Counter,Public,State,Deleted,ApplicationUserID,CouncilID")] Problem problem)
        {
            if (id != problem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(problem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemExists(problem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CouncilID"] = new SelectList(_context.Council, "CouncilID", "CouncilID", problem.CouncilID);
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUser, "Id", "Id", problem.ApplicationUserID);
            return View(problem);
        }

        // GET: Problems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problems
                .Include(p => p.Council)
                .Include(p => p.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (problem == null)
            {
                return NotFound();
            }

            return View(problem);
        }

        // POST: Problems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var problem = await _context.Problems.SingleOrDefaultAsync(m => m.Id == id);
            _context.Problems.Remove(problem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProblemExists(int id)
        {
            return _context.Problems.Any(e => e.Id == id);
        }
    }
}
