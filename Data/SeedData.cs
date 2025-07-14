using Microsoft.EntityFrameworkCore;
using ResourceBookingSystem.Models;

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
                    new Resource
                    {
                        Name = "Meeting Room Alpha",
                        Description = "Large room with projector and whiteboard",
                        Location = "3rd Floor, West Wing",
                        Capacity = 10,
                        IsAvailable = true
                    },
                    new Resource
                    {
                        Name = "Company Car 1",
                        Description = "Compact sedan",
                        Location = "Parking Bay 5",
                        Capacity = 4,
                        IsAvailable = true
                    },
                    new Resource
                    {
                        Name = "VR Equipment Set",
                        Description = "Virtual Reality headset with controllers",
                        Location = "Tech Lab",
                        Capacity = 1,
                        IsAvailable = true
                    }
                );

                context.SaveChanges();
            }
        }
    }
}