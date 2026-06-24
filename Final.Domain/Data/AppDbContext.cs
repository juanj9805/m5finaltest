using Final.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Final.Domain.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<WhishList> WhishLists { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Payments> Payments { get; set; }
    public DbSet<Bookings> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Room>()
            .HasOne(r => r.Hotel)
            .WithMany(h => h.Rooms)
            .HasForeignKey(r => r.HotelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Room>()
            .Property(r => r.DayBasePrice);

        builder.Entity<Bookings>()
            .HasOne(b => b.Room)
            .WithMany(r => r.Bookings)
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Bookings>()
            .HasOne(b => b.Client)
            .WithMany(c => c.Bookings)
            .HasForeignKey(b => b.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Payments>()
            .HasOne(p => p.Booking)
            .WithOne(b => b.Payment)
            .HasForeignKey<Payments>(p => p.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Payments>().Property(p => p.SubTotal);
        builder.Entity<Payments>().Property(p => p.Tax);
        builder.Entity<Payments>().Property(p => p.Total);

        builder.Entity<WhishList>()
            .HasOne(w => w.Hotel)
            .WithMany(h => h.WhishLists)
            .HasForeignKey(w => w.HotelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<WhishList>()
            .HasOne(w => w.Client)
            .WithMany(c => c.WhishLists)
            .HasForeignKey(w => w.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<WhishList>()
            .HasIndex(w => new { w.ClientId, w.HotelId })
            .IsUnique();
    }
}
