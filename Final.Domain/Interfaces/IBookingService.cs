using Final.Domain.Models.Entities;

namespace Final.Domain.Interfaces;

public interface IBookingService
{
    Task<Bookings> CreateAsync(Bookings bookings);
    Task<List<Bookings>> GetAllAsync();
    Task<Bookings?> GetByIdAsync(int id);
    Task<Bookings?> UpdateAsync(int id, Bookings bookings);
    Task<bool> DeleteAsync(int id);
}
