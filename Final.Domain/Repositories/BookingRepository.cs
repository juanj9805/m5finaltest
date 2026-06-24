using Final.Domain.Data;
using Final.Domain.Interfaces;
using Final.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Final.Domain.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Bookings> CreateAsync(Bookings bookings)
    {
        _context.Bookings.Add(bookings);
        await _context.SaveChangesAsync();
        return bookings;
    }

    public async Task<List<Bookings>> GetAllAsync()
    {
        return await _context.Bookings
            .Include(b => b.Room)
            .Include(b => b.Client)
            .Include(b => b.Payment)
            .ToListAsync();
    }

    public async Task<Bookings?> GetByIdAsync(int id)
    {
        return await _context.Bookings
            .Include(b => b.Room)
            .Include(b => b.Client)
            .Include(b => b.Payment)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Bookings?> UpdateAsync(int id, Bookings bookings)
    {
        var found = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        if (found == null)
        {
            return null;
        }

        found.RoomId = bookings.RoomId;
        found.ClientId = bookings.ClientId;
        found.StartDate = bookings.StartDate;
        found.EndDate = bookings.EndDate;
        found.Status = bookings.Status;

        await _context.SaveChangesAsync();
        return found;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var found = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        if (found == null)
        {
            return false;
        }

        _context.Bookings.Remove(found);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> HasOverlapAsync(int roomId, DateTime startDate, DateTime endDate)
    {
        return await _context.Bookings.AnyAsync(b =>
            b.RoomId == roomId &&
            startDate < b.EndDate &&
            b.StartDate < endDate);
    }
}
