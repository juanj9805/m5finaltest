namespace Final.Domain.Models.Entities;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string? IdentityUserId { get; set; }

    public List<Bookings> Bookings { get; set; } = new();
    public List<WhishList> WhishLists { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
