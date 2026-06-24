using Final.Domain.Interfaces;
using Final.Domain.Models.Entities;

namespace Final.Domain.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _repository;

    public RoomService(IRoomRepository repository)
    {
        _repository = repository;
    }

    public async Task<Room> CreateAsync(Room room)
    {
        return await _repository.CreateAsync(room);
    }

    public async Task<List<Room>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Room?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Room?> UpdateAsync(int id, Room room)
    {
        return await _repository.UpdateAsync(id, room);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
