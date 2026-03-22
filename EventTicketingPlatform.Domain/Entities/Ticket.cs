
namespace EventTicketingPlatform.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string TicketCode { get; set; }
        public string QRCode { get; set; }
        public DateTime IssueAt { get; set; } = DateTime.UtcNow;
        public DateTime? UsedAt { get; set; }
        public string Status { get; set; } = "Available";

        public int EventId { get; set; }
        public int OrderId { get; set; }

        public Event Event { get; set; }
        public Order Order { get; set; }
    }
}
