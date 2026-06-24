namespace Final.Domain.Models.Entities;

public class Bookings
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public int ClientId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string Status { get; set; } = "Confirmed";

    public Room? Room { get; set; }
    public Client? Client { get; set; }
    public Payments? Payment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
