namespace Final.Domain.Models.Entities;

public class Payments
{
    public int Id { get; set; }
    public int BookingId { get; set; }

    public decimal SubTotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }

    public Bookings? Booking { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
