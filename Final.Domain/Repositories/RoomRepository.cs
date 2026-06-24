using Final.Domain.Data;
using Final.Domain.Interfaces;
using Final.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Final.Domain.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _context;

    public RoomRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Room> CreateAsync(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return room;
    }

    public async Task<List<Room>> GetAllAsync()
    {
        return await _context.Rooms
            .Include(r => r.Hotel)
            .ToListAsync();
    }

    public async Task<Room?> GetByIdAsync(int id)
    {
        return await _context.Rooms
            .Include(r => r.Hotel)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Room?> UpdateAsync(int id, Room room)
    {
        var found = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
        if (found == null)
        {
            return null;
        }

        found.HotelId = room.HotelId;
        found.Number = room.Number;
        found.Capacity = room.Capacity;
        found.DayBasePrice = room.DayBasePrice;

        await _context.SaveChangesAsync();
        return found;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var found = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
        if (found == null)
        {
            return false;
        }

        _context.Rooms.Remove(found);
        await _context.SaveChangesAsync();
        return true;
    }
}
