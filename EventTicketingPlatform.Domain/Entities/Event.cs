
namespace EventTicketingPlatform.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public decimal PricePerTicket { get; set; }
        public int TotalCapacity { get; set; }
        public int AvailableTickets { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        public int OrganizerId { get; set; }
        public int CategoryId { get; set; }

        public User Organizer { get; set; }
        public Category Category { get; set; }
        public List<Ticket> Tickets { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
    }
}
