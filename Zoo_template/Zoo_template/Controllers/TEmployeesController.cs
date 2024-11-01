using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zoo_template.Models;
using System.Drawing.Printing;

namespace Zoo_template.Controllers
{
    public class TEmployeesController : Controller
    {
        private readonly ZooContext _context;
        private int pageSize = 3;

        public TEmployeesController(ZooContext context)
        {
            _context = context;
        }

        // GET: TEmployees
        public async Task<IActionResult> Index()
        {
            var zooContext = _context.TEmployees.Include(t => t.ResAreaNavigation).Include(t => t.Shift);
            int pageNum = (int)Math.Ceiling(zooContext.Count() / (float)pageSize);
            ViewBag.pageNum = pageNum;
            var result = zooContext.Take(pageSize).ToList();
            return View(result);
        }
        public IActionResult EmployeeFilter(string? keyword, int? pageIndex)
        {
            IQueryable<TEmployee> employees = _context.TEmployees;

            int page = (int)(pageIndex == null || pageIndex == 0 ? 1 : pageIndex);
            if (!string.IsNullOrEmpty(keyword))
            {
                employees = employees.Where(a => a.Name.ToLower().Contains(keyword.ToLower()));
                ViewBag.keyword = keyword;
            }

            int pageNum = (int)Math.Ceiling(employees.Count() / (float)pageSize);
            ViewBag.pageNum = pageNum;
            var result = employees.Skip(pageSize * (page - 1)).Take(pageSize).ToList();

            return PartialView("EmployeeTable", result);
        }


        // GET: TEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tEmployee = await _context.TEmployees
                .Include(t => t.ResAreaNavigation)
                .Include(t => t.Shift)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (tEmployee == null)
            {
                return NotFound();
            }

            return View(tEmployee);
        }

        // GET: TEmployees/Create
        public IActionResult Create()
        {
            var maxEmployeeId = _context.TEmployees.Max(e => e.EmployeeId);

            // Tạo một đối tượng TEmployee mới với EmployeeId tự động tăng
            var newEmployee = new TEmployee
            {
                EmployeeId = maxEmployeeId + 1
            };

            ViewData["ResArea"] = new SelectList(_context.TAreas, "AreaId", "AreaName", newEmployee.ResArea);
            ViewData["ShiftId"] = new SelectList(_context.TShifts, "ShiftId", "Name",newEmployee.ShiftId);
            return View(newEmployee);
        }


        // POST: TEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Name,Gender,PhoneNumber,Address,ResArea,ShiftId,OnWork")] TEmployee tEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResArea"] = new SelectList(_context.TAreas, "AreaId", "AreaName", tEmployee.ResArea);
            ViewData["ShiftId"] = new SelectList(_context.TShifts, "ShiftId", "Name", tEmployee.ShiftId);
            return View(tEmployee);
        }

        // GET: TEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tEmployee = await _context.TEmployees.FindAsync(id);
            if (tEmployee == null)
            {
                return NotFound();
            }
            ViewData["ResArea"] = new SelectList(_context.TAreas, "AreaId", "AreaName", tEmployee.ResArea);
            ViewData["ShiftId"] = new SelectList(_context.TShifts, "ShiftId", "Name", tEmployee.ShiftId);
            return View(tEmployee);
        }

        // POST: TEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,Name,Gender,PhoneNumber,Address,ResArea,ShiftId,OnWork")] TEmployee tEmployee)
        {
            if (id != tEmployee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TEmployeeExists(tEmployee.EmployeeId))
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
            ViewData["ResArea"] = new SelectList(_context.TAreas, "AreaId", "AreaName", tEmployee.ResArea);
            ViewData["ShiftId"] = new SelectList(_context.TShifts, "ShiftId", "Name", tEmployee.ShiftId);
            return View(tEmployee);
        }

        // GET: TEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tEmployee = await _context.TEmployees
                .Include(t => t.ResAreaNavigation)
                .Include(t => t.Shift)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (tEmployee == null)
            {
                return NotFound();
            }

            return View(tEmployee);
        }

        // POST: TEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tEmployee = await _context.TEmployees.FindAsync(id);
            if (tEmployee != null)
            {
                _context.TEmployees.Remove(tEmployee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TEmployeeExists(int id)
        {
            return _context.TEmployees.Any(e => e.EmployeeId == id);
        }
    }
}
