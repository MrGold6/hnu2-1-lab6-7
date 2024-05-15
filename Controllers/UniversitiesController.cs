using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabW4.Data;
using LabW4.Models;

namespace LabW4.Controllers
{
    public class UniversitiesController : Controller
    {
        private readonly LabW4Context _context;

        public UniversitiesController(LabW4Context context)
        {
            _context = context;
        }

        // GET: Universities
        public async Task<IActionResult> Index()
        {
            //14. Сформувати запити засобами методів розширення
            var labW4Context = _context.University
                .Include(u => u.TradeUnion);
            return View(
                await labW4Context
                .OrderBy(u => u.Name)
                .ToListAsync()
            );
        }

        //10. Використовуючи оператор where забезпечити відображення інформації, відібраної запевним критерієм.
        // GET: Universities/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.University
                .Include(u => u.TradeUnion)
                .Where(m => m.UniversityId == id)
                .FirstOrDefaultAsync();
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        // GET: Universities/Create
        public IActionResult Create()
        {
            ViewData["TradeUnionId"] = new SelectList(_context.TradeUnion, "TradeUnionId", "TradeUnionId");
            return View();
        }

        // POST: Universities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UniversityId,Count,IsCertified,PhoneNumber,Name,TradeUnionId")] University university)
        {
            _context.Add(university);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Universities/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.University.FindAsync(id);
            if (university == null)
            {
                return NotFound();
            }
            ViewData["TradeUnionId"] = new SelectList(_context.TradeUnion, "TradeUnionId", "TradeUnionId", university.TradeUnionId);
            return View(university);
        }

        // POST: Universities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UniversityId,Count,IsCertified,PhoneNumber,Name,TradeUnionId")] University university)
        {
            if (id != university.UniversityId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(university);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UniversityExists(university.UniversityId))
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

        // GET: Universities/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.University
                .Include(u => u.TradeUnion)
                .FirstOrDefaultAsync(m => m.UniversityId == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        // POST: Universities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var university = await _context.University.FindAsync(id);
            if (university != null)
            {
                _context.University.Remove(university);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UniversityExists(string id)
        {
            return _context.University.Any(e => e.UniversityId == id);
        }
    }
}
