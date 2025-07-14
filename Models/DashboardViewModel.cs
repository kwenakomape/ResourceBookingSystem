using System.Collections.Generic;

namespace ResourceBookingSystem.Models
{
    public class DashboardViewModel
    {
        public List<Booking> TodaysBookings { get; set; }
        public List<Booking> UpcomingBookings { get; set; }
        public List<Resource> AvailableResources { get; set; }
    }
}