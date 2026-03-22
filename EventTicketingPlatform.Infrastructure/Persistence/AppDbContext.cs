using EventTicketingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTicketingPlatform.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Role>().HasKey(r => r.Id);

            modelBuilder.Entity<Role>().Property(r => r.Name).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<User>()
           .HasKey(u => u.Id);
            
            modelBuilder.Entity<User>()
           .Property(u => u.Email)
           .IsRequired()
           .HasMaxLength(255);

            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

            modelBuilder.Entity<User>()
            .Property(u => u.PasswordHash)
            .IsRequired();

            modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
            .HasKey(c => c.Id);

            modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

            modelBuilder.Entity<Event>()
            .HasKey(e => e.Id);

            modelBuilder.Entity<Event>()
            .Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

            modelBuilder.Entity<Event>()
            .Property(e => e.PricePerTicket)
            .HasPrecision(10, 2);

            modelBuilder.Entity<Event>()
            .HasOne(e => e.Organizer)
            .WithMany(u => u.OrganizedEvents)
            .HasForeignKey(e => e.OrganizerId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
            .HasOne(e => e.Category)
            .WithMany(c => c.Events)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
            .HasKey(o => o.Id);

            modelBuilder.Entity<Order>()
            .Property(o => o.OrderNumber)
            .IsRequired()
            .HasMaxLength(50);

            modelBuilder.Entity<Order>()
            .HasIndex(o => o.OrderNumber)
            .IsUnique();

            modelBuilder.Entity<Order>()
            .Property(o => o.TotalPrice)
            .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
            .HasOne(o => o.Event)
            .WithMany(e => e.Orders)
            .HasForeignKey(o => o.EventId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
            .HasKey(t => t.Id);

            modelBuilder.Entity<Ticket>()
            .Property(t => t.TicketCode)
            .IsRequired()
            .HasMaxLength(50);

            modelBuilder.Entity<Ticket>()
            .HasIndex(t => t.TicketCode)
            .IsUnique();

            modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Event)
            .WithMany(e => e.Tickets)
            .HasForeignKey(t => t.EventId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Order)
            .WithMany(o => o.Tickets)
            .HasForeignKey(t => t.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
            .HasKey(r => r.Id);

            modelBuilder.Entity<Review>()
            .Property(r => r.Rating)
            .IsRequired();

            modelBuilder.Entity<Review>()
            .HasOne(r => r.Event)
            .WithMany(e => e.Reviews)
            .HasForeignKey(r => r.EventId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }   
}