using Final.Domain.Interfaces;
using Final.Domain.Models.Entities;

namespace Final.Domain.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _repository;

    public HotelService(IHotelRepository repository)
    {
        _repository = repository;
    }

    public async Task<Hotel> CreateAsync(Hotel hotel)
    {
        return await _repository.CreateAsync(hotel);
    }

    public async Task<List<Hotel>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Hotel?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Hotel?> UpdateAsync(int id, Hotel hotel)
    {
        return await _repository.UpdateAsync(id, hotel);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
