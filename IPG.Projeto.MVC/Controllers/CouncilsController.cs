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
    public class CouncilsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CouncilsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Councils
        public async Task<IActionResult> Index()
        {
            return View(await _context.Council.ToListAsync());
        }

        // GET: Councils/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var council = await _context.Council
                .SingleOrDefaultAsync(m => m.CouncilID == id);
            if (council == null)
            {
                return NotFound();
            }

            return View(council);
        }

        // GET: Councils/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Councils/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CouncilID,Id,Name,Reported,ReportedFix,Deleted,ExternalUrl,Email")] Council council)
        {
            if (ModelState.IsValid)
            {
                _context.Add(council);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(council);
        }

        // GET: Councils/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var council = await _context.Council.SingleOrDefaultAsync(m => m.CouncilID == id);
            if (council == null)
            {
                return NotFound();
            }
            return View(council);
        }

        // POST: Councils/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CouncilID,Id,Name,Reported,ReportedFix,Deleted,ExternalUrl,Email")] Council council)
        {
            if (id != council.CouncilID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(council);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CouncilExists(council.CouncilID))
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
            return View(council);
        }

        // GET: Councils/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var council = await _context.Council
                .SingleOrDefaultAsync(m => m.CouncilID == id);
            if (council == null)
            {
                return NotFound();
            }

            return View(council);
        }

        // POST: Councils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var council = await _context.Council.SingleOrDefaultAsync(m => m.CouncilID == id);
            _context.Council.Remove(council);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CouncilExists(int id)
        {
            return _context.Council.Any(e => e.CouncilID == id);
        }
    }
}
