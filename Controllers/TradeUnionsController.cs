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
    public class TradeUnionsController : Controller
    {
        private readonly LabW4Context _context;

        public TradeUnionsController(LabW4Context context)
        {
            _context = context;
        }

        // GET: TradeUnions
        public async Task<IActionResult> Index()
        {
            return View(await _context.TradeUnion.ToListAsync());
        }

        //10. Використовуючи оператор where забезпечити відображення інформації, відібраної запевним критерієм.
        // GET: TradeUnions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tradeUnion = await _context.TradeUnion
                                .Where(m => m.TradeUnionId == id)
                                .FirstOrDefaultAsync();

            if (tradeUnion == null)
            {
                return NotFound();
            }

            return View(tradeUnion);
        }

        // GET: TradeUnions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TradeUnions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TradeUnionId,Name,Count,DateOfCreation,Document,IsApproved")] TradeUnion tradeUnion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tradeUnion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tradeUnion);
        }

        // GET: TradeUnions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tradeUnion = await _context.TradeUnion.FindAsync(id);
            if (tradeUnion == null)
            {
                return NotFound();
            }
            return View(tradeUnion);
        }

        // POST: TradeUnions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TradeUnionId,Name,Count,DateOfCreation,Document,IsApproved")] TradeUnion tradeUnion)
        {
            if (id != tradeUnion.TradeUnionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tradeUnion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TradeUnionExists(tradeUnion.TradeUnionId))
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
            return View(tradeUnion);
        }

        // GET: TradeUnions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tradeUnion = await _context.TradeUnion
                .FirstOrDefaultAsync(m => m.TradeUnionId == id);
            if (tradeUnion == null)
            {
                return NotFound();
            }

            return View(tradeUnion);
        }

        // POST: TradeUnions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tradeUnion = await _context.TradeUnion.FindAsync(id);
            if (tradeUnion != null)
            {
                _context.TradeUnion.Remove(tradeUnion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TradeUnionExists(int id)
        {
            return _context.TradeUnion.Any(e => e.TradeUnionId == id);
        }
    }
}
