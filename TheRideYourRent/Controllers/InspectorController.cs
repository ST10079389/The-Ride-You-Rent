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
    public class InspectorController : Controller
    {
        private readonly TheRideYourRentContext _context;

        public InspectorController(TheRideYourRentContext context)
        {
            _context = context;
        }

        // GET: Inspector
        public async Task<IActionResult> Index()
        {
              return _context.Inspectors != null ? 
                          View(await _context.Inspectors.ToListAsync()) :
                          Problem("Entity set 'TheRideYourRentContext.Inspectors'  is null.");
        }

        // GET: Inspector/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inspectors == null)
            {
                return NotFound();
            }

            var inspector = await _context.Inspectors
                .FirstOrDefaultAsync(m => m.InspectorId == id);
            if (inspector == null)
            {
                return NotFound();
            }

            return View(inspector);
        }

        // GET: Inspector/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inspector/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InspectorId,InspectorNo,Name,Email,Mobile")] Inspector inspector)
        {
            if (ModelState.IsValid)
            {
                // Check if the inspector number already exists
                bool inspectorExists = await _context.Inspectors.AnyAsync(i => i.InspectorNo == inspector.InspectorNo);

                if (inspectorExists)
                {
                    ModelState.AddModelError("InspectorNo", "Inspector number already exists.");
                }
                else
                {
                    _context.Add(inspector);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(inspector);
        }


        // GET: Inspector/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inspectors == null)
            {
                return NotFound();
            }

            var inspector = await _context.Inspectors.FindAsync(id);
            if (inspector == null)
            {
                return NotFound();
            }
            return View(inspector);
        }

        // POST: Inspector/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InspectorId,InspectorNo,Name,Email,Mobile")] Inspector inspector)
        {
            if (id != inspector.InspectorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inspector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InspectorExists(inspector.InspectorId))
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
            return View(inspector);
        }

        // GET: Inspector/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inspectors == null)
            {
                return NotFound();
            }

            var inspector = await _context.Inspectors
                .FirstOrDefaultAsync(m => m.InspectorId == id);
            if (inspector == null)
            {
                return NotFound();
            }

            return View(inspector);
        }
        // POST: Inspector/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Inspectors == null)
                {
                    return Problem("Entity set 'TheRideYourRentContext.Inspectors' is null.");
                }

                var inspector = await _context.Inspectors.FindAsync(id);
                if (inspector != null)
                {
                    _context.Inspectors.Remove(inspector);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var inspector = await _context.Inspectors.FindAsync(id);
                TempData["ErrorMessage"] = "Can't delete inspector which has already inspected a car";
                return View(inspector);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool InspectorExists(int id)
        {
          return (_context.Inspectors?.Any(e => e.InspectorId == id)).GetValueOrDefault();
        }
    }
}
