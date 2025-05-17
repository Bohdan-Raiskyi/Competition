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
    public class DistancesController : Controller
    {
        private readonly SwimmingCompetitionDbContext _context;

        public DistancesController(SwimmingCompetitionDbContext context)
        {
            _context = context;
        }

        // GET: Distances
        public async Task<IActionResult> Index()
        {
            return View(await _context.Distances.ToListAsync());
        }

        // GET: Distances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distance = await _context.Distances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (distance == null)
            {
                return NotFound();
            }

            return View(distance);
        }

        // GET: Distances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Distances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Style,Length")] Distance distance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(distance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(distance);
        }

        // GET: Distances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distance = await _context.Distances.FindAsync(id);
            if (distance == null)
            {
                return NotFound();
            }
            return View(distance);
        }

        // POST: Distances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Style,Length")] Distance distance)
        {
            if (id != distance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(distance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistanceExists(distance.Id))
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
            return View(distance);
        }

        // GET: Distances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distance = await _context.Distances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (distance == null)
            {
                return NotFound();
            }

            return View(distance);
        }

        // POST: Distances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var distance = await _context.Distances.FindAsync(id);
            if (distance != null)
            {
                _context.Distances.Remove(distance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DistanceExists(int id)
        {
            return _context.Distances.Any(e => e.Id == id);
        }
    }
}
