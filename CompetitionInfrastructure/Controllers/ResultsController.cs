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
    public class ResultsController : Controller
    {
        private readonly SwimmingCompetitionDbContext _context;

        public ResultsController(SwimmingCompetitionDbContext context)
        {
            _context = context;
        }

        // GET: Results
        public async Task<IActionResult> Index()
        {
            var swimmingCompetitionDbContext = _context.Results.Include(r => r.Swim).Include(r => r.Swimmer);
            return View(await swimmingCompetitionDbContext.ToListAsync());
        }

        // GET: Results/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.Swim)
                .Include(r => r.Swimmer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Results/Create
        public IActionResult Create()
        {
            ViewData["SwimId"] = new SelectList(_context.Swims, "Id", "SwimNumber");
            ViewData["SwimmerId"] = new SelectList(_context.Swimmers, "Id", "Name");
            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SwimId,SwimmerId,ArrivalTime,ResultTime,Place")] Result result)
        {
            if (ModelState.IsValid)
            {
                _context.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SwimId"] = new SelectList(_context.Swims, "Id", "SwimNumber", result.SwimId);
            ViewData["SwimmerId"] = new SelectList(_context.Swimmers, "Id", "Name", result.SwimmerId);
            return View(result);
        }

        // GET: Results/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            ViewData["SwimId"] = new SelectList(_context.Swims, "Id", "SwimNumber", result.SwimId);
            ViewData["SwimmerId"] = new SelectList(_context.Swimmers, "Id", "Name", result.SwimmerId);
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SwimId,SwimmerId,ArrivalTime,ResultTime,Place")] Result result)
        {
            if (id != result.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(result.Id))
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
            ViewData["SwimId"] = new SelectList(_context.Swims, "Id", "SwimNumber", result.SwimId);
            ViewData["SwimmerId"] = new SelectList(_context.Swimmers, "Id", "Name", result.SwimmerId);
            return View(result);
        }

        // GET: Results/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.Swim)
                .Include(r => r.Swimmer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _context.Results.FindAsync(id);
            if (result != null)
            {
                _context.Results.Remove(result);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultExists(int id)
        {
            return _context.Results.Any(e => e.Id == id);
        }
    }
}
