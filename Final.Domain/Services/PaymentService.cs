using Final.Domain.Interfaces;
using Final.Domain.Models.Entities;

namespace Final.Domain.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;

    public PaymentService(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Payments> CreateAsync(Payments payments)
    {
        return await _repository.CreateAsync(payments);
    }

    public async Task<List<Payments>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Payments?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

}
