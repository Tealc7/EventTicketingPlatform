
namespace EventTicketingPlatform.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedDate { get; set; }
        public string Status { get; set; } = "Pending";
        public string PaymentMethod { get; set; }
        public string PaymentId { get; set; }


        public int UserId { get; set; }
        public int EventId { get; set; }

        public User User { get; set; }
        public Event Event { get; set; }
        public List<Ticket> Tickets { get; set; } = new();
    }
}