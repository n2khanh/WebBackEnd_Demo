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
    public class TAnimalsController : Controller
    {
        private readonly ZooContext _context;

        public TAnimalsController(ZooContext context)
        {
            _context = context;
        }

        // GET: TAnimals
        public async Task<IActionResult> Index()
        {
            var zooContext = _context.TAnimals.Include(t => t.Cage).Include(t => t.Food).Include(t => t.Species);
            return View(await zooContext.ToListAsync());
        }

        // GET: TAnimals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tAnimal = await _context.TAnimals
                .Include(t => t.Cage)
                .Include(t => t.Food)
                .Include(t => t.Species)
                .FirstOrDefaultAsync(m => m.AnimalId == id);
            if (tAnimal == null)
            {
                return NotFound();
            }

            return View(tAnimal);
        }

        // GET: TAnimals/Create
        public IActionResult Create()
        {
            ViewData["CageId"] = new SelectList(_context.TCages, "CageId", "CageId");
            ViewData["FoodId"] = new SelectList(_context.TFoods, "FoodId", "FoodId");
            ViewData["SpeciesId"] = new SelectList(_context.TSpecies, "SpeciesId", "SpeciesId");
            return View();
        }

        // POST: TAnimals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalId,Name,ScienName,TimeIn,TimeOut,Age,SpeciesId,CageId,Gender,Image,FoodId")] TAnimal tAnimal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tAnimal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CageId"] = new SelectList(_context.TCages, "CageId", "CageId", tAnimal.CageId);
            ViewData["FoodId"] = new SelectList(_context.TFoods, "FoodId", "FoodId", tAnimal.FoodId);
            ViewData["SpeciesId"] = new SelectList(_context.TSpecies, "SpeciesId", "SpeciesId", tAnimal.SpeciesId);
            return View(tAnimal);
        }

        // GET: TAnimals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tAnimal = await _context.TAnimals.FindAsync(id);
            if (tAnimal == null)
            {
                return NotFound();
            }
            ViewData["CageId"] = new SelectList(_context.TCages, "CageId", "CageId", tAnimal.CageId);
            ViewData["FoodId"] = new SelectList(_context.TFoods, "FoodId", "FoodId", tAnimal.FoodId);
            ViewData["SpeciesId"] = new SelectList(_context.TSpecies, "SpeciesId", "SpeciesId", tAnimal.SpeciesId);
            return View(tAnimal);
        }

        // POST: TAnimals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalId,Name,ScienName,TimeIn,TimeOut,Age,SpeciesId,CageId,Gender,Image,FoodId")] TAnimal tAnimal)
        {
            if (id != tAnimal.AnimalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tAnimal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TAnimalExists(tAnimal.AnimalId))
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
            ViewData["CageId"] = new SelectList(_context.TCages, "CageId", "CageId", tAnimal.CageId);
            ViewData["FoodId"] = new SelectList(_context.TFoods, "FoodId", "FoodId", tAnimal.FoodId);
            ViewData["SpeciesId"] = new SelectList(_context.TSpecies, "SpeciesId", "SpeciesId", tAnimal.SpeciesId);
            return View(tAnimal);
        }

        // GET: TAnimals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tAnimal = await _context.TAnimals
                .Include(t => t.Cage)
                .Include(t => t.Food)
                .Include(t => t.Species)
                .FirstOrDefaultAsync(m => m.AnimalId == id);
            if (tAnimal == null)
            {
                return NotFound();
            }

            return View(tAnimal);
        }

        // POST: TAnimals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tAnimal = await _context.TAnimals.FindAsync(id);
            if (tAnimal != null)
            {
                _context.TAnimals.Remove(tAnimal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TAnimalExists(int id)
        {
            return _context.TAnimals.Any(e => e.AnimalId == id);
        }
    }
}
