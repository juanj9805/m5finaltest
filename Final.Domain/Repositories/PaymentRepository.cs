using Final.Domain.Data;
using Final.Domain.Interfaces;
using Final.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Final.Domain.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Payments> CreateAsync(Payments payments)
    {
        _context.Payments.Add(payments);
        await _context.SaveChangesAsync();
        return payments;
    }

    public async Task<List<Payments>> GetAllAsync()
    {
        return await _context.Payments
            .Include(p => p.Booking)
            .ToListAsync();
    }

    public async Task<Payments?> GetByIdAsync(int id)
    {
        return await _context.Payments
            .Include(p => p.Booking)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Payments?> UpdateAsync(int id, Payments payments)
    {
        var found = await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);
        if (found == null)
        {
            return null;
        }

        found.SubTotal = payments.SubTotal;
        found.Tax = payments.Tax;
        found.Total = payments.Total;

        await _context.SaveChangesAsync();
        return found;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var found = await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);
        if (found == null)
        {
            return false;
        }

        _context.Payments.Remove(found);
        await _context.SaveChangesAsync();
        return true;
    }
}
