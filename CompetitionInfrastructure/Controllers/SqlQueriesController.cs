using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompetitionDomain.Model;
using CompetitionInfrastructure;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CompetitionInfrastructure.Controllers
{
    public class SqlQueriesController : Controller
    {
        private readonly SwimmingCompetitionDbContext _context;

        public SqlQueriesController(SwimmingCompetitionDbContext context)
        {
            _context = context;
        }

        // GET: SqlQueries
        public async Task<IActionResult> Index()
        {
            return View(await _context.SqlQuery.ToListAsync());
        }

        // GET: SqlQueries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sqlQuery = await _context.SqlQuery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sqlQuery == null)
            {
                return NotFound();
            }

            return View(sqlQuery);
        }

        // GET: SqlQueries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SqlQueries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,QueryText")] SqlQuery sqlQuery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sqlQuery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sqlQuery);
        }

        // GET: SqlQueries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sqlQuery = await _context.SqlQuery.FindAsync(id);
            if (sqlQuery == null)
            {
                return NotFound();
            }
            return View(sqlQuery);
        }

        // POST: SqlQueries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,QueryText")] SqlQuery sqlQuery)
        {
            if (id != sqlQuery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sqlQuery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SqlQueryExists(sqlQuery.Id))
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
            return View(sqlQuery);
        }

        // GET: SqlQueries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sqlQuery = await _context.SqlQuery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sqlQuery == null)
            {
                return NotFound();
            }

            return View(sqlQuery);
        }

        // POST: SqlQueries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sqlQuery = await _context.SqlQuery.FindAsync(id);
            if (sqlQuery != null)
            {
                _context.SqlQuery.Remove(sqlQuery);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SqlQueryExists(int id)
        {
            return _context.SqlQuery.Any(e => e.Id == id);
        }
        /////////////////////////////////////////////////////////////////////////////////////////

        // GET: Виконання запиту (форма для параметрів)
        public IActionResult Execute(int? id)
        {
            if (id == null) return NotFound();

            var sqlQuery = _context.SqlQuery.Find(id);
            if (sqlQuery == null) return NotFound();

            // Витягуємо параметри з тексту запиту (наприклад, @maxPlace)
            var parameters = ExtractParameters(sqlQuery.QueryText);
            ViewBag.Parameters = parameters;
            ViewBag.QueryId = id;

            return View(sqlQuery);
        }

        // POST: Виконання запиту (обробка параметрів)
        [HttpPost]
        public IActionResult Execute(int id, Dictionary<string, string> parameters)
        {
            var sqlQuery = _context.SqlQuery.Find(id);
            if (sqlQuery == null) return NotFound();

            try
            {
                DataTable dataTable = new DataTable(); // System.Data.DataTable
                using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(sqlQuery.QueryText, connection))
                    {
                        foreach (var param in parameters)
                            command.Parameters.AddWithValue(param.Key, param.Value);

                        using (var reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader); // Завантаження даних у DataTable
                        }
                    }
                }
                return View("Results", dataTable);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        // Допоміжний метод для витягування параметрів (наприклад, @maxPlace)
        private List<string> ExtractParameters(string sql)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"@\w+");
            return regex.Matches(sql)
                .Cast<System.Text.RegularExpressions.Match>()
                .Select(m => m.Value)
                .Distinct()
                .ToList();
        }
    }
}

