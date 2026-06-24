using Final.Domain.Data;
using Final.Domain.Interfaces;
using Final.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Final.Domain.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly AppDbContext _context;

    public HotelRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Hotel> CreateAsync(Hotel hotel)
    {
        _context.Hotels.Add(hotel);
        await _context.SaveChangesAsync();
        return hotel;
    }

    public async Task<List<Hotel>> GetAllAsync()
    {
        return await _context.Hotels
            .Include(h => h.Rooms)
            .ToListAsync();
    }

    public async Task<Hotel?> GetByIdAsync(int id)
    {
        return await _context.Hotels
            .Include(h => h.Rooms)
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<Hotel?> UpdateAsync(int id, Hotel hotel)
    {
        var found = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
        if (found == null)
        {
            return null;
        }

        found.Name = hotel.Name;
        found.Address = hotel.Address;
        found.City = hotel.City;
        found.TotalRooms = hotel.TotalRooms;

        await _context.SaveChangesAsync();
        return found;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var found = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
        if (found == null)
        {
            return false;
        }

        _context.Hotels.Remove(found);
        await _context.SaveChangesAsync();
        return true;
    }
}
