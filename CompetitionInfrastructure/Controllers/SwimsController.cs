using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompetitionDomain.Model;
using CompetitionInfrastructure;

namespace CompetitionInfrastructure.Controllers
{
    public class SwimsController : Controller
    {
        private readonly SwimmingCompetitionDbContext _context;

        public SwimsController(SwimmingCompetitionDbContext context)
        {
            _context = context;
        }

        // GET: Swims
        public async Task<IActionResult> Index()
        {
            var swimmingCompetitionDbContext = _context.Swims.Include(s => s.Competition).Include(s => s.Distance);
            return View(await swimmingCompetitionDbContext.ToListAsync());
        }

        // GET: Swims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swim = await _context.Swims
                .Include(s => s.Competition)
                .Include(s => s.Distance)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (swim == null)
            {
                return NotFound();
            }

            return View(swim);
        }

        // GET: Swims/Create
        public IActionResult Create()
        {
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "Id", "Venue");
            ViewData["DistanceId"] = new SelectList(_context.Distances, "Id", "Style");
            return View();
        }

        // POST: Swims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DistanceId,CompetitionId,SwimNumber,StartTime")] Swim swim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(swim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "Id", "Venue", swim.CompetitionId);
            ViewData["DistanceId"] = new SelectList(_context.Distances, "Id", "Style", swim.DistanceId);
            return View(swim);
        }

        // GET: Swims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swim = await _context.Swims.FindAsync(id);
            if (swim == null)
            {
                return NotFound();
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "Id", "Venue", swim.CompetitionId);
            ViewData["DistanceId"] = new SelectList(_context.Distances, "Id", "Style", swim.DistanceId);
            return View(swim);
        }

        // POST: Swims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DistanceId,CompetitionId,SwimNumber,StartTime")] Swim swim)
        {
            if (id != swim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(swim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SwimExists(swim.Id))
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
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "Id", "Venue", swim.CompetitionId);
            ViewData["DistanceId"] = new SelectList(_context.Distances, "Id", "Style", swim.DistanceId);
            return View(swim);
        }

        // GET: Swims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swim = await _context.Swims
                .Include(s => s.Competition)
                .Include(s => s.Distance)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (swim == null)
            {
                return NotFound();
            }

            return View(swim);
        }

        // POST: Swims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var swim = await _context.Swims.FindAsync(id);
            if (swim != null)
            {
                _context.Swims.Remove(swim);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SwimExists(int id)
        {
            return _context.Swims.Any(e => e.Id == id);
        }
    }
}
