using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceBookingSystem.Data;
using ResourceBookingSystem.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var model = new DashboardViewModel
            {
                TodaysBookings = await _context.Bookings
                    .Include(b => b.Resource)
                    .Where(b => b.StartTime.Date == today)
                    .OrderBy(b => b.StartTime)
                    .ToListAsync(),

                UpcomingBookings = await _context.Bookings
                    .Include(b => b.Resource)
                    .Where(b => b.StartTime.Date > today && b.StartTime.Date <= today.AddDays(7))
                    .OrderBy(b => b.StartTime)
                    .Take(5)
                    .ToListAsync(),

                AvailableResources = await _context.Resources
                    .Where(r => r.IsAvailable)
                    .OrderBy(r => r.Name)
                    .Take(4)
                    .ToListAsync()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}