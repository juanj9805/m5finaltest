namespace Final.Domain.Models.Entities;

public class WhishList
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public int ClientId { get; set; }

    public Hotel? Hotel { get; set; }
    public Client? Client { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
