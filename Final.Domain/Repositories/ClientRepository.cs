using Final.Domain.Data;
using Final.Domain.Interfaces;
using Final.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Final.Domain.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Client> CreateAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<List<Client>> GetAllAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Client?> UpdateAsync(int id, Client client)
    {
        var found = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        if (found == null)
        {
            return null;
        }

        found.Name = client.Name;
        found.Email = client.Email;

        await _context.SaveChangesAsync();
        return found;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var found = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        if (found == null)
        {
            return false;
        }

        _context.Clients.Remove(found);
        await _context.SaveChangesAsync();
        return true;
    }
}
