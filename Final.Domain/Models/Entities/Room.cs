namespace Final.Domain.Models.Entities;

public class Room
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public string Number { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public decimal DayBasePrice { get; set; }

    public Hotel? Hotel { get; set; }
    public List<Bookings> Bookings { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
