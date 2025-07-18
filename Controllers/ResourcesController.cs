﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceBookingSystem.Data;
using ResourceBookingSystem.Models;


// Handles CRUD operations for Resources

namespace ResourceBookingSystem.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        //Constructor with dependency injection
      
        public ResourcesController(ApplicationDbContext context)
        {
            _context = context;
        }
        //GET: Resources list with optional search/filter

        public async Task<IActionResult> Index(string search, string availability)
        {
            var query = _context.Resources.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(r => r.Name.Contains(search) ||
                                       r.Description.Contains(search) ||
                                       r.Location.Contains(search));
            }

            if (!string.IsNullOrEmpty(availability))
            {
                var isAvailable = bool.Parse(availability);
                query = query.Where(r => r.IsAvailable == isAvailable);
            }

            return View(await query.ToListAsync());
        }

        // GET: Resource details including upcoming bookings

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources
                .Include(r => r.Bookings)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        // GET: Resources/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Resources/Create,  a user can creating a booking by submmiting relevant information
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Location,Capacity,IsAvailable")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resource);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Resource created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(resource);
        }
        // GET: Resources/Edit/id, use can view particular resource
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources.FindAsync(id);
            if (resource == null)
            {
                return NotFound();
            }
            return View(resource);
        }

        // GET: Resources/Edit/id, user can edit their booking incase they make a mistake or want to reschedule
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Location,Capacity,IsAvailable")] Resource resource)
        {
            if (id != resource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceExists(resource.Id))
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
            return View(resource);
        }

        // GET: Resources/Delete/id, user can view deatils of the specific resource
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        // GET: Resources/Delete/id, user can delete resource
        [HttpPost]
        [ValidateAntiForgeryToken]  // protect againts cors

        // User cannot delete resource that has been booked
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var resource = await _context.Resources
                    .Include(r => r.Bookings)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (resource == null)
                {
                    return Json(new { success = false, message = "Resource not found." });
                }

                if (resource.Bookings.Any())
                {
                    return Json(new
                    {
                        success = false,
                        message = "Cannot delete resource with existing bookings."
                    });
                }

                _context.Resources.Remove(resource);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting resource: {ex}");
                return Json(new
                {
                    success = false,
                    message = "An error occurred while deleting the resource."
                });
            }
        }

        private bool ResourceExists(int id)
        {
            return _context.Resources.Any(e => e.Id == id);
        }
    }
}