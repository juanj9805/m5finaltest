using Final.Domain.Models.Entities;

namespace Final.Domain.Interfaces;

public interface IHotelService
{
    Task<Hotel> CreateAsync(Hotel hotel);
    Task<List<Hotel>> GetAllAsync();
    Task<Hotel?> GetByIdAsync(int id);
    Task<Hotel?> UpdateAsync(int id, Hotel hotel);
    Task<bool> DeleteAsync(int id);
}
