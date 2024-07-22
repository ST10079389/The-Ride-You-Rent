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
    public class ReturnController : Controller
    {
        private readonly TheRideYourRentContext _context;

        public ReturnController(TheRideYourRentContext context)
        {
            _context = context;
        }

        // GET: Return
        public async Task<IActionResult> Index()
        {
            var theRideYourRentContext = _context.Returns.Include(a => a.CarNoNavigation).Include(a => a.Driver).Include(a => a.InspectorNoNavigation).Include(a => a.Make);
            return View(await theRideYourRentContext.ToListAsync());
        }

        // GET: Return/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Returns == null)
            {
                return NotFound();
            }

            var @return = await _context.Returns
                .Include(a => a.CarNoNavigation)
                .Include(a => a.Driver)
                .Include(a => a.InspectorNoNavigation)
                .Include(a => a.Make)
                .FirstOrDefaultAsync(m => m.ReturnId == id);
            if (@return == null)
            {
                return NotFound();
            }

            return View(@return);
        }

        // GET: Return/Create
        public IActionResult Create()
        {
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo");
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name");
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "Name");
            ViewData["MakeId"] = new SelectList(_context.CarMakes, "MakeId", "Description");
            return View();
        }

        // POST: Return/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReturnId,ReturnDate,Fine,CarNo,InspectorNo,DriverId,MakeId")] Return @return)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var rental = await _context.Rentals
                        .FirstOrDefaultAsync(r => r.CarNo == @return.CarNo && r.DriverId == @return.DriverId && r.InspectorNo == @return.InspectorNo);

                    if (rental != null)
                    {
                        var elapsedDays = (@return.ReturnDate - rental.EndDate)?.TotalDays;

                        if (elapsedDays != null)
                        {
                            var fine = (int)(elapsedDays * 500);

                            @return.ElapsedDate = (int)elapsedDays;
                            @return.Fine = fine;

                            _context.Add(@return);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            throw new Exception("Invalid return date.");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid, please ensure the data matches the Rental before creating the Return.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo", @return.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name", @return.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "Name", @return.InspectorNo);
            ViewData["MakeId"] = new SelectList(_context.CarMakes, "MakeId", "Description", @return.MakeId);

            return View(@return);
        }
        // GET: Return/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Returns == null)
            {
                return NotFound();
            }

            var @return = await _context.Returns.FindAsync(id);
            if (@return == null)
            {
                return NotFound();
            }
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo", @return.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name", @return.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "Name", @return.InspectorNo);
            ViewData["MakeId"] = new SelectList(_context.CarMakes, "MakeId", "Description", @return.MakeId);
            return View(@return);
        }

        // POST: Return/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReturnId,ReturnDate,ElapsedDate,Fine,CarNo,InspectorNo,DriverId,MakeId")] Return @return)
        {
            if (id != @return.ReturnId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@return);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReturnExists(@return.ReturnId))
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
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo", @return.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name", @return.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "Name", @return.InspectorNo);
            ViewData["MakeId"] = new SelectList(_context.CarMakes, "MakeId", "Description", @return.MakeId);
            return View(@return);
        }

        // GET: Return/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Returns == null)
            {
                return NotFound();
            }

            var @return = await _context.Returns
                .Include(a => a.CarNoNavigation)
                .Include(a => a.Driver)
                .Include(a => a.InspectorNoNavigation)
                .Include(a => a.Make)
                .FirstOrDefaultAsync(m => m.ReturnId == id);
            if (@return == null)
            {
                return NotFound();
            }

            return View(@return);
        }

        // POST: Return/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Returns == null)
            {
                return Problem("Entity set 'TheRideYourRentContext.Returns'  is null.");
            }
            var @return = await _context.Returns.FindAsync(id);
            if (@return != null)
            {
                _context.Returns.Remove(@return);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReturnExists(int id)
        {
          return (_context.Returns?.Any(e => e.ReturnId == id)).GetValueOrDefault();
        }
    }
}
