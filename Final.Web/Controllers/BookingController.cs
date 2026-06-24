using Final.Domain.Exceptions;
using Final.Domain.Interfaces;
using Final.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Final.Web.Controllers;

public class BookingController : Controller
{
    private readonly IBookingService _bookingService;
    private readonly IRoomService _roomService;
    private readonly IClientService _clientService;

    public BookingController(IBookingService bookingService, IRoomService roomService, IClientService clientService)
    {
        _bookingService = bookingService;
        _roomService = roomService;
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var bookings = await _bookingService.GetAllAsync();
        return View(bookings);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var booking = await _bookingService.GetByIdAsync(id);
        if (booking is null) return NotFound();
        return View(booking);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Create(int? roomId)
    {
        await PopulateListsAsync(roomId);
        return View(new Bookings
        {
            RoomId = roomId ?? 0,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(1)
        });
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Bookings bookings)
    {
        if (!ModelState.IsValid)
        {
            await PopulateListsAsync(bookings.RoomId, bookings.ClientId);
            return View(bookings);
        }

        try
        {
            await _bookingService.CreateAsync(bookings);
        }
        catch (BookingConflictException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await PopulateListsAsync(bookings.RoomId, bookings.ClientId);
            return View(bookings);
        }

        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _bookingService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateListsAsync(int? selectedRoom = null, int? selectedClient = null)
    {
        var rooms = await _roomService.GetAllAsync();
        var clients = await _clientService.GetAllAsync();

        ViewBag.Rooms = new SelectList(rooms, "Id", "Number", selectedRoom);
        ViewBag.Clients = new SelectList(clients, "Id", "Name", selectedClient);
    }
}
