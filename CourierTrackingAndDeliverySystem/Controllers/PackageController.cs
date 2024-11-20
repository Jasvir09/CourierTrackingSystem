using CourierTrackingAndDeliverySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourierTrackingAndDeliverySystem.Controllers
{
    public class PackageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PackageController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var packages = _context.Packages.ToList();
            return View(packages);
        }
        public IActionResult Create()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.Users = _context.Users.ToList();
                return View();
            }
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Package package)
        {
            if (package.AssignedTo.HasValue)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == package.AssignedTo.Value);
                if (user != null)
                {
                    package.AssignedUser = user;  
                }
            }
            if (ModelState.IsValid)
            {
                package.LastUpdated = DateTime.Now;
                _context.Packages.Add(package);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
                return View(package);
        }

        // Action to display the tracking form
        public IActionResult Track()
        {
            return View();
        }

        // Action to handle tracking number submission
        [HttpPost]
        public IActionResult Track(string trackingNumber)
        {
            if (string.IsNullOrEmpty(trackingNumber))
            {
                ViewBag.Error = "Please enter a valid tracking number.";
                return View();
            }

            var package = _context.Packages.FirstOrDefault(p => p.TrackingNumber == trackingNumber);

            if (package == null)
            {
                ViewBag.Error = "Package not found.";
                return View();
            }

            // If the package is found, pass it to the view
            return View("TrackResult", package);
        }
        // Action to display the package status
    public IActionResult TrackResult(string trackingNumber)
    {
        // Fetch package from database using the tracking number
        var package = _context.Packages.FirstOrDefault(p => p.TrackingNumber == trackingNumber);

        if (package == null)
        {
            ViewBag.Error = "Package not found.";
            return View("Track");
        }

        // Return the TrackResult view with the package details
        return View(package);
    }
        public IActionResult UpdateStatus(int id)
        {
            var package = _context.Packages.Find(id);
            if (package == null)
            {
                return NotFound();
            }

            ViewBag.StatusList = new List<string>
    {
        "Delivered",
        "In Transit",
        "Pending"
    };

            return View(package);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(int id, string newStatus)
        {
            var package = _context.Packages.Find(id);
            if (package == null)
            {
                return NotFound();
            }

            package.CurrentStatus = newStatus;
            package.LastUpdated = DateTime.Now;

            _context.Packages.Update(package);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
