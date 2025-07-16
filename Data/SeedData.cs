using Microsoft.EntityFrameworkCore;
using ResourceBookingSystem.Models;

// Here I'm seeding initial data to the database

namespace ResourceBookingSystem.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Resources.Any())
                {
                    return; // DB has been seeded
                }

                context.Resources.AddRange(
    // Conference/Meeting Resources
    new Resource
    {
        Name = "Meeting Room Alpha",
        Description = "Large conference room with 4K projector, 75\" smart screen, and whiteboard. Includes video conferencing equipment.",
        Location = "3rd Floor, West Wing, Room 301",
        Capacity = 12,
        IsAvailable = true
    },
    new Resource
    {
        Name = "Huddle Room Beta",
        Description = "Small collaborative space with 55\" touchscreen and phone system",
        Location = "2nd Floor, North Wing, Room 210",
        Capacity = 6,
        IsAvailable = true
    },
    new Resource
    {
        Name = "Executive Boardroom",
        Description = "Premium meeting space with leather seating, 85\" 8K display, and premium audio system",
        Location = "1st Floor, Executive Suite",
        Capacity = 16,
        IsAvailable = true
    },

    // Transportation Resources
    new Resource
    {
        Name = "Company Car 1 (Toyota Camry)",
        Description = "2024 Toyota Camry Hybrid - Silver. Includes EZ-Pass and GPS navigation.",
        Location = "Parking Bay 5",
        Capacity = 4,
        IsAvailable = true
    },
    new Resource
    {
        Name = "Company Van",
        Description = "2023 Ford Transit - White. Suitable for equipment transport (max load 1500lbs).",
        Location = "Parking Bay 12",
        Capacity = 8,
        IsAvailable = true
    },
    new Resource
    {
        Name = "Executive Sedan (Mercedes E-Class)",
        Description = "2024 Mercedes-Benz E350 - Black. For client meetings and executive transport.",
        Location = "Underground Garage A1",
        Capacity = 4,
        IsAvailable = false, // Currently in maintenance
    },

    // Technology Equipment
    new Resource
    {
        Name = "VR Development Kit",
        Description = "Oculus Quest Pro with controllers and development SDK access",
        Location = "Tech Lab, Drawer 3A",
        Capacity = 1,
        IsAvailable = true
    },
    new Resource
    {
        Name = "Video Production Kit",
        Description = "Sony A7IV camera, 3-lens set, tripod, and audio equipment",
        Location = "Media Closet B",
        Capacity = 1,
        IsAvailable = true
    },
    new Resource
    {
        Name = "Portable Projector",
        Description = "Epson EB-1781W 1080p wireless projector with carrying case",
        Location = "IT Equipment Room",
        Capacity = 1,
        IsAvailable = true
    },

    // Specialized Equipment
    new Resource
    {
        Name = "3D Printer",
        Description = "Ultimaker S5 with dual extrusion and air filtration system",
        Location = "Maker Lab",
        Capacity = 1,
        IsAvailable = true
    },
    new Resource
    {
        Name = "Laser Cutter",
        Description = "Glowforge Pro 40W with exhaust ventilation",
        Location = "Maker Lab",
        Capacity = 1,
        IsAvailable = false, // Currently in use for training
        // MaintenanceNote: "Reserved for employee training until 2025-07-18"
    },
    new Resource
    {
        Name = "Drone (DJI Mavic 3)",
        Description = "4K aerial photography drone with 3 batteries and carrying case",
        Location = "Media Closet A",
        Capacity = 1,
        IsAvailable = true,
        // SpecialRequirement: "Requires FAA Part 107 certification"
    },

    // Office Resources
    new Resource
    {
        Name = "Mobile Whiteboard",
        Description = "Double-sided rolling whiteboard with markers and erasers",
        Location = "Shared Equipment Area",
        Capacity = 1,
        IsAvailable = true
    },
    new Resource
    {
        Name = "Portable Sound Booth",
        Description = "WhisperRoom Model 4244 for professional audio recording",
        Location = "Media Production Room",
        Capacity = 1,
        IsAvailable = true
    },
    new Resource
    {
        Name = "Hot Desk Station 14",
        Description = "Adjustable standing desk with dual monitors in coworking space",
        Location = "3rd Floor, Open Workspace",
        Capacity = 1,
        IsAvailable = true
    }
);

                context.SaveChanges();
            }
        }
    }
}