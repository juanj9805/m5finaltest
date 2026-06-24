using Final.Domain.Models.Entities;

namespace Final.Domain.Interfaces;

public interface IPaymentRepository
{
    Task<Payments> CreateAsync(Payments payments);
    Task<List<Payments>> GetAllAsync();
    Task<Payments?> GetByIdAsync(int id);
    Task<Payments?> UpdateAsync(int id, Payments payments);
    Task<bool> DeleteAsync(int id);
}
