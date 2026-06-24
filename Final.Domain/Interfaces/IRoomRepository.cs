using Final.Domain.Models.Entities;

namespace Final.Domain.Interfaces;

public interface IRoomRepository
{
    Task<Room> CreateAsync(Room room);
    Task<List<Room>> GetAllAsync();
    Task<Room?> GetByIdAsync(int id);
    Task<Room?> UpdateAsync(int id, Room room);
    Task<bool> DeleteAsync(int id);
}
