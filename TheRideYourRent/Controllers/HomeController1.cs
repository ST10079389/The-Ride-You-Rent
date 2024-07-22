using Microsoft.AspNetCore.Mvc;
using TheRideYourRent.Models;

namespace TheRideYourRent.Controllers
{
    public class HomeController1 : Controller
    {
        private readonly TheRideYourRentContext _context;
        public HomeController1(TheRideYourRentContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([Bind("Name,Password")] InspectorRegister inspectorRegister)
        {
            var user = _context.InspectorRegisters
                .FirstOrDefault(x => x.Name == inspectorRegister.Name && x.Password == inspectorRegister.Password);

            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid credentials");
                return View(inspectorRegister);
            }
        }
    }
}
