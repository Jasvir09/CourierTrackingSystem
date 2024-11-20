using CourierTrackingAndDeliverySystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CourierTrackingAndDeliverySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, string newStatus)
        {
            var package = _context.Packages.Find(id);
            if (package != null)
            {
                package.CurrentStatus = newStatus;
                _context.SaveChanges();
                return RedirectToAction("Track", new { trackingNumber = package.TrackingNumber });
            }
            return NotFound();
        }

        public IActionResult Index()
        {
            var IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            var report = new
            {
                TotalDelivered = _context.Packages.Count(p => p.CurrentStatus == "Delivered"),
                TotalInTransit = _context.Packages.Count(p => p.CurrentStatus == "In Transit"),
                TotalPending = _context.Packages.Count(p => p.CurrentStatus == "Pending"),
            };
            return View(report);
        }
    }
}
