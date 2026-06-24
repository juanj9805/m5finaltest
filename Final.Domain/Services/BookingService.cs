using Final.Domain.Exceptions;
using Final.Domain.Interfaces;
using Final.Domain.Models.Entities;

namespace Final.Domain.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repository;
    private readonly IRoomRepository _roomRepository;

    public BookingService(IBookingRepository repository, IRoomRepository roomRepository)
    {
        _repository = repository;
        _roomRepository = roomRepository;
    }

    public async Task<Bookings> CreateAsync(Bookings bookings)
    {
        bookings.StartDate = bookings.StartDate.Date.AddHours(14);
        bookings.EndDate = bookings.EndDate.Date.AddHours(12);

        if (bookings.EndDate <= bookings.StartDate)
        {
            throw new BookingConflictException("The check-out date must be after the check-in date.");
        }

        var room = await _roomRepository.GetByIdAsync(bookings.RoomId);
        if (room == null)
        {
            throw new BookingConflictException("The selected room does not exist.");
        }

        var isBusy = await _repository.HasOverlapAsync(bookings.RoomId, bookings.StartDate, bookings.EndDate);
        if (isBusy)
        {
            throw new BookingConflictException("The room is already booked for the selected dates.");
        }

        var nights = (bookings.EndDate.Date - bookings.StartDate.Date).Days;
        if (nights < 1)
        {
            nights = 1;
        }

        var subTotal = nights * room.DayBasePrice;
        var tax = subTotal * 0.19m;

        bookings.Status = "Confirmed";
        bookings.Payment = new Payments
        {
            SubTotal = subTotal,
            Tax = tax,
            Total = subTotal + tax
        };

        return await _repository.CreateAsync(bookings);
    }

    public async Task<List<Bookings>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Bookings?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Bookings?> UpdateAsync(int id, Bookings bookings)
    {
        bookings.StartDate = bookings.StartDate.Date.AddHours(14);
        bookings.EndDate = bookings.EndDate.Date.AddHours(12);
        return await _repository.UpdateAsync(id, bookings);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
