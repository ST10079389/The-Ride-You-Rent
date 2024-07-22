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
    public class DriverController : Controller
    {
        private readonly TheRideYourRentContext _context;

        public DriverController(TheRideYourRentContext context)
        {
            _context = context;
        }

        // GET: Driver
        public async Task<IActionResult> Index()
        {
              return _context.Drivers != null ? 
                          View(await _context.Drivers.ToListAsync()) :
                          Problem("Entity set 'TheRideYourRentContext.Drivers'  is null.");
        }

        // GET: Driver/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Drivers == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // GET: Driver/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Driver/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DriverId,Name,Address,Email,Mobile")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                // Check if the driver already exists
                bool driverExists = await _context.Drivers.AnyAsync(d =>
                    d.Name == driver.Name &&
                    d.Address == driver.Address &&
                    d.Email == driver.Email &&
                    d.Mobile == driver.Mobile);

                if (driverExists)
                {
                    ModelState.AddModelError("", "Driver already exists.");
                }
                else
                {
                    _context.Add(driver);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(driver);
        }
        // GET: Driver/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Drivers == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            return View(driver);
        }

        // POST: Driver/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DriverId,Name,Address,Email,Mobile")] Driver driver)
        {
            if (id != driver.DriverId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverExists(driver.DriverId))
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
            return View(driver);
        }

        // GET: Driver/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Drivers == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // POST: Driver/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Drivers == null)
                {
                    return Problem("Entity set 'TheRideYourRentContext.Drivers' is null.");
                }

                var driver = await _context.Drivers.FindAsync(id);
                var isUsedInRental = await _context.Rentals.AnyAsync(r => r.DriverId == id);

                if (driver != null)
                {
                    if (isUsedInRental)
                    {
                        throw new Exception("Can't delete drivers that have already rented a car");
                    }

                    _context.Drivers.Remove(driver);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var driver = await _context.Drivers.FindAsync(id);
                TempData["ErrorMessage"] = ex.Message;
                return View(driver);
            }

            return RedirectToAction(nameof(Index));
        }



        private bool DriverExists(int id)
        {
          return (_context.Drivers?.Any(e => e.DriverId == id)).GetValueOrDefault();
        }
    }
}
