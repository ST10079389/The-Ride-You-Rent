using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheRideYourRent.Models;

namespace TheRideYourRent.Controllers
{
    public class InspectorRegistersController : Controller
    {
        private readonly TheRideYourRentContext _context;

        public InspectorRegistersController(TheRideYourRentContext context)
        {
            _context = context;
        }

        // GET: InspectorRegisters
        public async Task<IActionResult> Index()
        {
              return _context.InspectorRegisters != null ? 
                          View(await _context.InspectorRegisters.ToListAsync()) :
                          Problem("Entity set 'TheRideYourRentContext.InspectorRegisters'  is null.");
        }

        // GET: InspectorRegisters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InspectorRegisters == null)
            {
                return NotFound();
            }

            var inspectorRegister = await _context.InspectorRegisters
                .FirstOrDefaultAsync(m => m.InspectorId == id);
            if (inspectorRegister == null)
            {
                return NotFound();
            }

            return View(inspectorRegister);
        }

        // GET: InspectorRegisters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InspectorRegisters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InspectorId,Name,Password")] InspectorRegister inspectorRegister)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inspectorRegister);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inspectorRegister);
        }

        // GET: InspectorRegisters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InspectorRegisters == null)
            {
                return NotFound();
            }

            var inspectorRegister = await _context.InspectorRegisters.FindAsync(id);
            if (inspectorRegister == null)
            {
                return NotFound();
            }
            return View(inspectorRegister);
        }

        // POST: InspectorRegisters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InspectorId,Name,Password")] InspectorRegister inspectorRegister)
        {
            if (id != inspectorRegister.InspectorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inspectorRegister);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InspectorRegisterExists(inspectorRegister.InspectorId))
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
            return View(inspectorRegister);
        }

        // GET: InspectorRegisters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InspectorRegisters == null)
            {
                return NotFound();
            }

            var inspectorRegister = await _context.InspectorRegisters
                .FirstOrDefaultAsync(m => m.InspectorId == id);
            if (inspectorRegister == null)
            {
                return NotFound();
            }

            return View(inspectorRegister);
        }

        // POST: InspectorRegisters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InspectorRegisters == null)
            {
                return Problem("Entity set 'TheRideYourRentContext.InspectorRegisters'  is null.");
            }
            var inspectorRegister = await _context.InspectorRegisters.FindAsync(id);
            if (inspectorRegister != null)
            {
                _context.InspectorRegisters.Remove(inspectorRegister);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InspectorRegisterExists(int id)
        {
          return (_context.InspectorRegisters?.Any(e => e.InspectorId == id)).GetValueOrDefault();
        }
    }
}
