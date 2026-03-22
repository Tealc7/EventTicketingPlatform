
namespace EventTicketingPlatform.Domain.Entities
{
    public class User
    {
        public int Id { get; set; } 
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        public int RoleId { get; set; }


        public Role Role { get; set; }
        public List<Event> OrganizedEvents { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();

    }
}
