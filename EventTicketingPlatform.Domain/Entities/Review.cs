
namespace EventTicketingPlatform.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public int EventId { get; set; }
        public int UserId { get; set; }

        public Event Event { get; set; }
        public User User { get; set; }
    }
}
