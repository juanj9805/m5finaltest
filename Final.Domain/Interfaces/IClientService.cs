using Final.Domain.Models.Entities;

namespace Final.Domain.Interfaces;

public interface IClientService
{
    Task<Client> CreateAsync(Client client);
    Task<List<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(int id);
    Task<Client?> UpdateAsync(int id, Client client);
    Task<bool> DeleteAsync(int id);
}
