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
    public class CarController : Controller
    {
        private readonly TheRideYourRentContext _context;

        public CarController(TheRideYourRentContext context)
        {
            _context = context;
        }

        // GET: Car
        public async Task<IActionResult> Index()
        {
            var theRideYourRentContext = _context.Cars.Include(c => c.BodyType).Include(c => c.Make);
            return View(await theRideYourRentContext.ToListAsync());
        }

        // GET: Car/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.BodyType)
                .Include(c => c.Make)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Car/Create
        public IActionResult Create()
        {
            ViewData["BodyTypeId"] = new SelectList(_context.CarBodyTypes, "BodyTypeId", "Description");
            ViewData["MakeId"] = new SelectList(_context.CarMakes, "MakeId", "Description");
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,CarNo,Model,KilometresTravelled,ServiceKilometres,Available,MakeId,BodyTypeId")] Car car)
        {
            if (ModelState.IsValid)
            {
                // Check if the car number already exists
                bool carExists = await _context.Cars.AnyAsync(c => c.CarNo == car.CarNo);
                if (carExists)
                {
                    ModelState.AddModelError("CarNo", "Car number already exists.");
                }
                else
                {
                    _context.Add(car);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["BodyTypeId"] = new SelectList(_context.CarBodyTypes, "BodyTypeId", "Description", car.BodyTypeId);
            ViewData["MakeId"] = new SelectList(_context.CarMakes, "MakeId", "Description", car.MakeId);
            return View(car);
        }

        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["BodyTypeId"] = new SelectList(_context.CarBodyTypes, "BodyTypeId", "Description", car.BodyTypeId);
            ViewData["MakeId"] = new SelectList(_context.CarMakes, "MakeId", "Description", car.MakeId);
            return View(car);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,CarNo,Model,KilometresTravelled,ServiceKilometres,Available,MakeId,BodyTypeId")] Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
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
            ViewData["BodyTypeId"] = new SelectList(_context.CarBodyTypes, "BodyTypeId", "Description", car.BodyTypeId);
            ViewData["MakeId"] = new SelectList(_context.CarMakes, "MakeId", "Description", car.MakeId);
            return View(car);
        }

        // GET: Car/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.BodyType)
                .Include(c => c.Make)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars
                .Include(c => c.Make)
                .Include(c => c.BodyType)
                .FirstOrDefaultAsync(m => m.CarId == id);

            if (car == null)
            {
                return NotFound();
            }

            // Remove associated records in Rental table
            var rentals = _context.Rentals.Where(r => r.CarNo == car.CarNo);
            _context.Rentals.RemoveRange(rentals);

            // Remove associated records in Return table
            var returns = _context.Returns.Where(r => r.CarNo == car.CarNo);
            _context.Returns.RemoveRange(returns);

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool CarExists(int id)
        {
          return (_context.Cars?.Any(e => e.CarId == id)).GetValueOrDefault();
        }
    }
}
