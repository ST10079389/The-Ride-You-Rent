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
    public class RentalController : Controller
    {
        private readonly TheRideYourRentContext _context;

        public RentalController(TheRideYourRentContext context)
        {
            _context = context;
        }

        // GET: Rental
        public async Task<IActionResult> Index()
        {
            var theRideYourRentContext = _context.Rentals.Include(r => r.CarNoNavigation).Include(r => r.Driver).Include(r => r.InspectorNoNavigation).Include(r => r.Make);
            return View(await theRideYourRentContext.ToListAsync());
        }

        // GET: Rental/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rentals == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.CarNoNavigation)
                .Include(r => r.Driver)
                .Include(r => r.InspectorNoNavigation)
                .Include(r => r.Make)
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rental/Create
        public IActionResult Create()
        {
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo");
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name");
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "Name");
            ViewData["MakeId"] = new SelectList(_context.CarMakes, "MakeId", "Description");
            return View();
        }

        // POST: Rental/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalId,RentalFee,StartDate,EndDate,CarNo,InspectorNo,DriverId,MakeId")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo", rental.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name", rental.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "Name", rental.InspectorNo);
            ViewData["MakeId"] = new SelectList(_context.CarMakes, "MakeId", "Description", rental.MakeId);
            return View(rental);
        }

        // GET: Rental/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rentals == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo", rental.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name", rental.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "Name", rental.InspectorNo);
            ViewData["MakeId"] = new SelectList(_context.CarMakes, "MakeId", "Description", rental.MakeId);
            return View(rental);
        }

        // POST: Rental/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentalId,RentalFee,StartDate,EndDate,CarNo,InspectorNo,DriverId,MakeId")] Rental rental)
        {
            if (id != rental.RentalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.RentalId))
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
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo", rental.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name", rental.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "Name", rental.InspectorNo);
            ViewData["MakeId"] = new SelectList(_context.CarMakes, "MakeId", "Description", rental.MakeId);
            return View(rental);
        }

        // GET: Rental/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rentals == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.CarNoNavigation)
                .Include(r => r.Driver)
                .Include(r => r.InspectorNoNavigation)
                .Include(r => r.Make)
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rentals == null)
            {
                return Problem("Entity set 'TheRideYourRentContext.Rentals'  is null.");
            }
            var rental = await _context.Rentals.FindAsync(id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
          return (_context.Rentals?.Any(e => e.RentalId == id)).GetValueOrDefault();
        }
    }
}
