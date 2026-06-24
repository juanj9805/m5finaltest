using Final.Domain.Models.Entities;

namespace Final.Domain.Interfaces;

public interface IPaymentService
{
    Task<Payments> CreateAsync(Payments payments);
    Task<List<Payments>> GetAllAsync();
    Task<Payments?> GetByIdAsync(int id);
}
