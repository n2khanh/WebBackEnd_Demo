using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zoo_template.Models;

namespace Zoo_template.Controllers
{
    public class TCagesController : Controller
    {
        private readonly ZooContext _context;

        public TCagesController(ZooContext context)
        {
            _context = context;
        }

        // GET: TCages
        public async Task<IActionResult> Index()
        {
            var zooContext = _context.TCages.Include(t => t.Area);
            return View(await zooContext.ToListAsync());
        }

        // GET: TCages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCage = await _context.TCages
                .Include(t => t.Area)
                .FirstOrDefaultAsync(m => m.CageId == id);
            if (tCage == null)
            {
                return NotFound();
            }

            return View(tCage);
        }

        // GET: TCages/Create
        public IActionResult Create()
        {
            ViewData["AreaId"] = new SelectList(_context.TAreas, "AreaId", "AreaName");
            return View();
        }

        // POST: TCages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CageId,AreaId,Quantity,MaxQuantity,Note")] TCage tCage)
        {
            if (ModelState.IsValid)
            {
                tCage.CageId = (_context.TCages.Max(a => (int?)a.CageId) ?? 0) + 1;
                _context.Add(tCage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreaId"] = new SelectList(_context.TAreas, "AreaId", "AreaName", tCage.AreaId);
            return View(tCage);
        }

        // GET: TCages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCage = await _context.TCages.FindAsync(id);
            if (tCage == null)
            {
                return NotFound();
            }
            ViewData["AreaId"] = new SelectList(_context.TAreas, "AreaId", "AreaId", tCage.AreaId);
            return View(tCage);
        }

        // POST: TCages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CageId,AreaId,Quantity,MaxQuantity,Note")] TCage tCage)
        {
            if (id != tCage.CageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tCage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TCageExists(tCage.CageId))
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
            ViewData["AreaId"] = new SelectList(_context.TAreas, "AreaId", "AreaId", tCage.AreaId);
            return View(tCage);
        }

        // GET: TCages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCage = await _context.TCages
                .Include(t => t.Area)
                .FirstOrDefaultAsync(m => m.CageId == id);
            if (tCage == null)
            {
                return NotFound();
            }

            return View(tCage);
        }

        // POST: TCages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tCage = await _context.TCages.FindAsync(id);
            if (tCage != null)
            {
                _context.TCages.Remove(tCage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TCageExists(int id)
        {
            return _context.TCages.Any(e => e.CageId == id);
        }
    }
}
