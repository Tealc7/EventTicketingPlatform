using EventTicketingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTicketingPlatform.Infrastructure.Persistence;

public static class SeedData
{
    public static async Task InitializeSeedDataAsync(this AppDbContext context)
    {
        await context.Database.MigrateAsync();

        if (context.Roles.Any())
            return;

        // ===== SEED ROLES =====
        var roles = new List<Role>
        {
            new() { Name = "Admin", Description = "Administrator of the platform" },
            new() { Name = "Organizer", Description = "Event organizer" },
            new() { Name = "Customer", Description = "Regular customer" }
        };
        await context.Roles.AddRangeAsync(roles);
        await context.SaveChangesAsync();

        // ===== SEED USERS =====
        var users = new List<User>
        {
            new()
            {
                Email = "admin@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                FirstName = "Admin",
                LastName = "User",
                PhoneNumber = "+421901234567",
                RoleId = roles.First(r => r.Name == "Admin").Id,
                IsActive = true
            },
            new()
            {
                Email = "organizer@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Organizer123!"),
                FirstName = "John",
                LastName = "Organizer",
                PhoneNumber = "+421902234567",
                RoleId = roles.First(r => r.Name == "Organizer").Id,
                IsActive = true
            },
            new()
            {
                Email = "customer1@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Customer123!"),
                FirstName = "Jane",
                LastName = "Customer",
                PhoneNumber = "+421903234567",
                RoleId = roles.First(r => r.Name == "Customer").Id,
                IsActive = true
            },
            new()
            {
                Email = "customer2@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Customer123!"),
                FirstName = "Bob",
                LastName = "Smith",
                PhoneNumber = "+421904234567",
                RoleId = roles.First(r => r.Name == "Customer").Id,
                IsActive = true
            }
        };
        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();

        // ===== SEED CATEGORIES =====
        var categories = new List<Category>
        {
            new()
            {
                Name = "Music",
                Description = "Music events and concerts",
                IconUrl = "https://via.placeholder.com/50?text=Music"
            },
            new()
            {
                Name = "Theater",
                Description = "Theater and drama performances",
                IconUrl = "https://via.placeholder.com/50?text=Theater"
            },
            new()
            {
                Name = "Cinema",
                Description = "Movie screenings",
                IconUrl = "https://via.placeholder.com/50?text=Cinema"
            },
            new()
            {
                Name = "Sports",
                Description = "Sports events",
                IconUrl = "https://via.placeholder.com/50?text=Sports"
            },
            new()
            {
                Name = "Conference",
                Description = "Business conferences and seminars",
                IconUrl = "https://via.placeholder.com/50?text=Conference"
            }
        };
        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();

        // ===== SEED EVENTS =====
        var organizer = users.First(u => u.Email == "organizer@example.com");
        var musicCategory = categories.First(c => c.Name == "Music");
        var sportsCategory = categories.First(c => c.Name == "Sports");
        var theaterCategory = categories.First(c => c.Name == "Theater");

        var events = new List<Event>
        {
            new()
            {
                Title = "Summer Music Festival 2026",
                Description = "Amazing summer festival with international artists",
                EventDate = DateTime.UtcNow.AddDays(45),
                EventEndDate = DateTime.UtcNow.AddDays(47),
                Location = "Bratislava, Slovakia",
                ImageUrl = "https://via.placeholder.com/400x300?text=Music+Festival",
                PricePerTicket = 49.99m,
                TotalCapacity = 1000,
                AvailableTickets = 1000,
                OrganizerId = organizer.Id,
                CategoryId = musicCategory.Id,
                IsActive = true
            },
            new()
            {
                Title = "Rock Night Live",
                Description = "Best rock bands performing live",
                EventDate = DateTime.UtcNow.AddDays(30),
                EventEndDate = DateTime.UtcNow.AddDays(30),
                Location = "Košice, Slovakia",
                ImageUrl = "https://via.placeholder.com/400x300?text=Rock+Night",
                PricePerTicket = 35.00m,
                TotalCapacity = 500,
                AvailableTickets = 500,
                OrganizerId = organizer.Id,
                CategoryId = musicCategory.Id,
                IsActive = true
            },
            new()
            {
                Title = "Football Championship",
                Description = "National football championship final",
                EventDate = DateTime.UtcNow.AddDays(60),
                EventEndDate = DateTime.UtcNow.AddDays(60),
                Location = "Žilina, Slovakia",
                ImageUrl = "https://via.placeholder.com/400x300?text=Football",
                PricePerTicket = 29.99m,
                TotalCapacity = 5000,
                AvailableTickets = 5000,
                OrganizerId = organizer.Id,
                CategoryId = sportsCategory.Id,
                IsActive = true
            },
            new()
            {
                Title = "Theater Production - Hamlet",
                Description = "Classic Shakespeare's Hamlet in modern interpretation",
                EventDate = DateTime.UtcNow.AddDays(20),
                EventEndDate = DateTime.UtcNow.AddDays(20),
                Location = "Bratislava, Slovakia",
                ImageUrl = "https://via.placeholder.com/400x300?text=Hamlet",
                PricePerTicket = 25.00m,
                TotalCapacity = 300,
                AvailableTickets = 300,
                OrganizerId = organizer.Id,
                CategoryId = theaterCategory.Id,
                IsActive = true
            }
        };
        await context.Events.AddRangeAsync(events);
        await context.SaveChangesAsync();

        // ===== SEED ORDERS =====
        var customer1 = users.First(u => u.Email == "customer1@example.com");
        var customer2 = users.First(u => u.Email == "customer2@example.com");
        var musicFestival = events.First(e => e.Title == "Summer Music Festival 2026");
        var rockNight = events.First(e => e.Title == "Rock Night Live");

        var orders = new List<Order>
        {
            new()
            {
                OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-001",
                UserId = customer1.Id,
                EventId = musicFestival.Id,
                TotalPrice = 149.97m,
                Quantity = 3,
                Status = "Paid",
                PaymentMethod = "Stripe",
                PaymentId = "pi_1234567890",
                OrderDate = DateTime.UtcNow.AddDays(-10),
                CompletedDate = DateTime.UtcNow.AddDays(-10)
            },
            new()
            {
                OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-002",
                UserId = customer2.Id,
                EventId = rockNight.Id,
                TotalPrice = 70.00m,
                Quantity = 2,
                Status = "Paid",
                PaymentMethod = "Stripe",
                PaymentId = "pi_0987654321",
                OrderDate = DateTime.UtcNow.AddDays(-5),
                CompletedDate = DateTime.UtcNow.AddDays(-5)
            }
        };
        await context.Orders.AddRangeAsync(orders);
        await context.SaveChangesAsync();

        // ===== SEED TICKETS =====
        var tickets = new List<Ticket>();

        // Tickets pre order 1
        for (int i = 1; i <= 3; i++)
        {
            tickets.Add(new()
            {
                TicketCode = $"T-{musicFestival.Id}-{orders[0].Id}-{i:000}",
                QRCode = $"QR-{Guid.NewGuid()}",
                EventId = musicFestival.Id,
                OrderId = orders[0].Id,
                Status = "Sold",
                IssueAt = DateTime.UtcNow.AddDays(-10)
            });
        }

        // Tickets pre order 2
        for (int i = 1; i <= 2; i++)
        {
            tickets.Add(new()
            {
                TicketCode = $"T-{rockNight.Id}-{orders[1].Id}-{i:000}",
                QRCode = $"QR-{Guid.NewGuid()}",
                EventId = rockNight.Id,
                OrderId = orders[1].Id,
                Status = "Sold",
                IssueAt = DateTime.UtcNow.AddDays(-5)
            });
        }

        await context.Tickets.AddRangeAsync(tickets);
        await context.SaveChangesAsync();

        // ===== SEED REVIEWS =====
        var reviews = new List<Review>
        {
            new()
            {
                EventId = musicFestival.Id,
                UserId = customer1.Id,
                Rating = 5,
                Comment = "Excellent festival! Great organization and amazing performances.",
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new()
            {
                EventId = rockNight.Id,
                UserId = customer2.Id,
                Rating = 4,
                Comment = "Very good concert. Sound could have been better.",
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            }
        };
        await context.Reviews.AddRangeAsync(reviews);
        await context.SaveChangesAsync();
    }
}