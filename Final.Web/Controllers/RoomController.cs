using Final.Domain.Interfaces;
using Final.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Final.Web.Controllers;

public class RoomController : Controller
{
    private readonly IRoomService _roomService;
    private readonly IHotelService _hotelService;

    public RoomController(IRoomService roomService, IHotelService hotelService)
    {
        _roomService = roomService;
        _hotelService = hotelService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var rooms = await _roomService.GetAllAsync();
        return View(rooms);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var room = await _roomService.GetByIdAsync(id);
        if (room is null) return NotFound();
        return View(room);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await PopulateHotelsAsync();
        return View(new Room());
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Room room)
    {
        if (!ModelState.IsValid)
        {
            await PopulateHotelsAsync(room.HotelId);
            return View(room);
        }

        await _roomService.CreateAsync(room);
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var room = await _roomService.GetByIdAsync(id);
        if (room is null) return NotFound();
        await PopulateHotelsAsync(room.HotelId);
        return View(room);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Room room)
    {
        if (!ModelState.IsValid)
        {
            await PopulateHotelsAsync(room.HotelId);
            return View(room);
        }

        var updated = await _roomService.UpdateAsync(id, room);
        if (updated is null) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _roomService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateHotelsAsync(int? selected = null)
    {
        var hotels = await _hotelService.GetAllAsync();
        ViewBag.Hotels = new SelectList(hotels, nameof(Hotel.Id), nameof(Hotel.Name), selected);
    }
}
