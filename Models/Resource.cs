using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//create Resource model (Id, Name, Description, etc.)
namespace ResourceBookingSystem.Models
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public string Description { get; set; }

        [StringLength(200, ErrorMessage = "Location cannot be longer than 200 characters")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be a positive number")]
        public int Capacity { get; set; }

        public bool IsAvailable { get; set; } = true;

        
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}