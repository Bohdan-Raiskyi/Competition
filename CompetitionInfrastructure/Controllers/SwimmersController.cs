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
    public class SwimmersController : Controller
    {
        private readonly SwimmingCompetitionDbContext _context;

        public SwimmersController(SwimmingCompetitionDbContext context)
        {
            _context = context;
        }

        // GET: Swimmers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Swimmers.ToListAsync());
        }

        // GET: Swimmers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swimmer = await _context.Swimmers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (swimmer == null)
            {
                return NotFound();
            }

            return View(swimmer);
        }

        // GET: Swimmers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Swimmers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TeamName,AgeCategory")] Swimmer swimmer)
        {
            // Перевірка діапазону років (якщо вказано)
            if (swimmer.AgeCategory.Contains("-"))
            {
                var years = swimmer.AgeCategory.Split('-');
                if (years.Length != 2 ||
                    !int.TryParse(years[0], out int startYear) ||
                    !int.TryParse(years[1], out int endYear) ||
                    startYear >= endYear)
                {
                    ModelState.AddModelError("AgeCategory", "Невірний діапазон років (наприклад, 1990-1991).");
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(swimmer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(swimmer);
        }

        // GET: Swimmers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swimmer = await _context.Swimmers.FindAsync(id);
            if (swimmer == null)
            {
                return NotFound();
            }
            return View(swimmer);
        }

        // POST: Swimmers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TeamName,AgeCategory")] Swimmer swimmer)
        {
            if (id != swimmer.Id)
            {
                return NotFound();
            }

            // Перевірка діапазону років (якщо вказано)
            if (swimmer.AgeCategory.Contains("-"))
            {
                var years = swimmer.AgeCategory.Split('-');
                if (years.Length != 2 ||
                    !int.TryParse(years[0], out int startYear) ||
                    !int.TryParse(years[1], out int endYear) ||
                    startYear >= endYear)
                {
                    ModelState.AddModelError("AgeCategory", "Невірний діапазон років (наприклад, 1990-1991).");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(swimmer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SwimmerExists(swimmer.Id))
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
            return View(swimmer);
        }

        // GET: Swimmers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swimmer = await _context.Swimmers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (swimmer == null)
            {
                return NotFound();
            }

            return View(swimmer);
        }

        // POST: Swimmers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var swimmer = await _context.Swimmers.FindAsync(id);
            if (swimmer != null)
            {
                _context.Swimmers.Remove(swimmer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SwimmerExists(int id)
        {
            return _context.Swimmers.Any(e => e.Id == id);
        }
    }
}
