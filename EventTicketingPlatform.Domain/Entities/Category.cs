
namespace EventTicketingPlatform.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public List<Event> Events { get; set; } = new();
    }
}
