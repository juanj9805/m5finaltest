using Final.Domain.Interfaces;
using Final.Domain.Models.Entities;

namespace Final.Domain.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;

    public ClientService(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Client> CreateAsync(Client client)
    {
        return await _repository.CreateAsync(client);
    }

    public async Task<List<Client>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Client?> UpdateAsync(int id, Client client)
    {
        return await _repository.UpdateAsync(id, client);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
