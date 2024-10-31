using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Index(int? id)
        {
            var zooContext = _context.TAnimals
                .Include(t => t.Cage)
                .Include(t => t.Food)
                .Include(t => t.Species)
                .Where(t => t.CageId == id);

            TempData["CageID"] = id;
            return View(await zooContext.ToListAsync());
        }

        // GET: TAnimals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var tAnimal = await _context.TAnimals
                .Include(t => t.Cage)
                .Include(t => t.Food)
                .Include(t => t.Species)
                .FirstOrDefaultAsync(m => m.AnimalId == id);

            if (tAnimal == null)
                return NotFound();

            return View(tAnimal);
        }

        public IActionResult Create()
        {
            // Ensure the model is not null
            var model = new TAnimal();
            PopulateViewData(model);
            return View(model);
        }

        // POST: TAnimals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ScienName,TimeIn,TimeOut,Age,SpeciesId,Gender,Image,FoodId")] TAnimal tAnimal)
        {
            if (ModelState.IsValid)
            {
                // Auto-generate AnimalId (consider using database auto-increment instead)
                tAnimal.CageId = (int?)TempData["CageID"];
                _context.Add(tAnimal);

                var cage = await _context.TCages.FindAsync(tAnimal.CageId);
                if (cage != null)
                {
                    cage.Quantity += 1;
                    _context.Update(cage);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = tAnimal.CageId });
            }

            PopulateViewData(tAnimal);
            return View(tAnimal);
        }

        // GET: TAnimals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var tAnimal = await _context.TAnimals.FindAsync(id);
            if (tAnimal == null)
                return NotFound();

            PopulateViewData(tAnimal);
            return View(tAnimal);
        }

        // POST: TAnimals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalId,Name,ScienName,TimeIn,TimeOut,Age,SpeciesId,CageId,Gender,Image,FoodId")] TAnimal tAnimal)
        {
            if (id != tAnimal.AnimalId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    tAnimal.CageId = (int?)TempData["CageID"];
                    _context.Update(tAnimal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TAnimalExists(tAnimal.AnimalId))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index), new { id = tAnimal.CageId });
            }

            PopulateViewData(tAnimal);
            return View(tAnimal);
        }

        // GET: TAnimals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var tAnimal = await _context.TAnimals
                .Include(t => t.Cage)
                .Include(t => t.Food)
                .Include(t => t.Species)
                .FirstOrDefaultAsync(m => m.AnimalId == id);

            if (tAnimal == null)
                return NotFound();

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
                var cage = await _context.TCages.FirstOrDefaultAsync(c => c.CageId == tAnimal.CageId);

                if (cage != null)
                {
                    cage.Quantity -= 1;
                    _context.Update(cage);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index), new { id = tAnimal.CageId });
        }

        private bool TAnimalExists(int id)
        {
            return _context.TAnimals.Any(e => e.AnimalId == id);
        }

        // File upload
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Json(new { success = false, message = "No file uploaded." });

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                return Json(new { success = false, message = "Invalid file type." });

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine("wwwroot/images", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return Json(new { success = true, fileName });
            }
            catch (Exception ex)
            {
                // Log the exception
                return Json(new { success = false, message = "File upload failed: " + ex.Message });
            }
        }

        private void PopulateViewData(TAnimal tAnimal = null)
        {
            ViewData["FoodId"] = new SelectList(_context.TFoods, "FoodId", "FoodName", tAnimal?.FoodId);
            ViewData["SpeciesId"] = new SelectList(_context.TSpecies, "SpeciesId", "SpeciesName", tAnimal?.SpeciesId);
        }
    }
}
