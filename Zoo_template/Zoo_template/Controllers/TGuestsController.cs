﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zoo_template.Models;

namespace Zoo_template.Controllers
{
    public class TGuestsController : Controller
    {
        private readonly ZooContext _context;

        public TGuestsController(ZooContext context)
        {
            _context = context;
        }

        // GET: TGuests
        public async Task<IActionResult> Index()
        {
            var zooContext = _context.TGuests.Include(t => t.PayMethodNavigation).Include(t => t.Ticket);
            return View(await zooContext.ToListAsync());
        }

        // GET: TGuests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGuest = await _context.TGuests
                .Include(t => t.PayMethodNavigation)
                .Include(t => t.Ticket)
                .FirstOrDefaultAsync(m => m.GuestId == id);
            if (tGuest == null)
            {
                return NotFound();
            }

            return View(tGuest);
        }

        // GET: TGuests/Create
        public IActionResult Create()
        {
            ViewData["PayMethod"] = new SelectList(_context.TPayMethods, "PayMethodId", "PayMethodId");
            ViewData["TicketId"] = new SelectList(_context.TTickets, "TicketId", "TicketId");
            return View();
        }

        // POST: TGuests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GuestId,GuestName,DateOfBirth,PayMethod,PhoneNumber,Address,TicketId")] TGuest tGuest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tGuest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PayMethod"] = new SelectList(_context.TPayMethods, "PayMethodId", "PayMethodId", tGuest.PayMethodID);
            ViewData["TicketId"] = new SelectList(_context.TTickets, "TicketId", "TicketId", tGuest.TicketId);
            return View(tGuest);
        }

        // GET: TGuests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGuest = await _context.TGuests.FindAsync(id);
            if (tGuest == null)
            {
                return NotFound();
            }
            ViewData["PayMethod"] = new SelectList(_context.TPayMethods, "PayMethodId", "PayMethodId", tGuest.PayMethodID);
            ViewData["TicketId"] = new SelectList(_context.TTickets, "TicketId", "TicketId", tGuest.TicketId);
            return View(tGuest);
        }

        // POST: TGuests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GuestId,GuestName,DateOfBirth,PayMethod,PhoneNumber,Address,TicketId")] TGuest tGuest)
        {
            if (id != tGuest.GuestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tGuest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TGuestExists(tGuest.GuestId))
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
            ViewData["PayMethod"] = new SelectList(_context.TPayMethods, "PayMethodId", "PayMethodId", tGuest.PayMethodID);
            ViewData["TicketId"] = new SelectList(_context.TTickets, "TicketId", "TicketId", tGuest.TicketId);
            return View(tGuest);
        }

        // GET: TGuests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGuest = await _context.TGuests
                .Include(t => t.PayMethodNavigation)
                .Include(t => t.Ticket)
                .FirstOrDefaultAsync(m => m.GuestId == id);
            if (tGuest == null)
            {
                return NotFound();
            }

            return View(tGuest);
        }

        // POST: TGuests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tGuest = await _context.TGuests.FindAsync(id);
            if (tGuest != null)
            {
                _context.TGuests.Remove(tGuest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TGuestExists(int id)
        {
            return _context.TGuests.Any(e => e.GuestId == id);
        }
    }
}