using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourceBookingSystem.Data;
using ResourceBookingSystem.Models;

namespace ResourceBookingSystem.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //feat: implement Booking CRUD with conflict detection
        public async Task<IActionResult> Index(string fromDate, string toDate)
        {
            var query = _context.Bookings.Include(b => b.Resource).AsQueryable();

            if (!string.IsNullOrEmpty(fromDate))
            {
                var from = DateTime.Parse(fromDate);
                query = query.Where(b => b.StartTime >= from);
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                var to = DateTime.Parse(toDate).AddDays(1);
                query = query.Where(b => b.StartTime <= to);
            }

            return View(await query.OrderBy(b => b.StartTime).ToListAsync());
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["ResourceId"] = new SelectList(_context.Resources.Where(r => r.IsAvailable), "Id", "Name");
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ResourceId,StartTime,EndTime,BookedBy,Purpose")] Booking booking)
        {
            
            bool hasConflict = await _context.Bookings
                .AnyAsync(b => b.ResourceId == booking.ResourceId &&
                    ((booking.StartTime >= b.StartTime && booking.StartTime < b.EndTime) ||
                     (booking.EndTime > b.StartTime && booking.EndTime <= b.EndTime) ||
                     (booking.StartTime <= b.StartTime && booking.EndTime >= b.EndTime)));

            if (hasConflict)
            {
                ModelState.AddModelError(string.Empty, "This resource is already booked during the requested time.");
            }

            if (booking.EndTime <= booking.StartTime)
            {
                ModelState.AddModelError("EndTime", "End time must be after start time.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ResourceId"] = new SelectList(_context.Resources.Where(r => r.IsAvailable), "Id", "Name", booking.ResourceId);
            return View(booking);
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Resource)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Name", booking.ResourceId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ResourceId,StartTime,EndTime,BookedBy,Purpose")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            bool hasConflict = await _context.Bookings
                .AnyAsync(b => b.Id != booking.Id &&
                              b.ResourceId == booking.ResourceId &&
                              ((booking.StartTime >= b.StartTime && booking.StartTime < b.EndTime) ||
                               (booking.EndTime > b.StartTime && booking.EndTime <= b.EndTime) ||
                               (booking.StartTime <= b.StartTime && booking.EndTime >= b.EndTime)));

            if (hasConflict)
            {
                ModelState.AddModelError(string.Empty, "This resource is already booked during the requested time.");
            }

            if (booking.EndTime <= booking.StartTime)
            {
                ModelState.AddModelError("EndTime", "End time must be after start time.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Name", booking.ResourceId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Resource)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var booking = await _context.Bookings.FindAsync(id);
                if (booking == null)
                {
                    return Json(new { success = false, message = "Booking not found." });
                }

                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Booking cancelled successfully";
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while cancelling the booking." });
            }
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}