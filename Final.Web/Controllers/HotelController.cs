using Final.Domain.Interfaces;
using Final.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final.Web.Controllers;

public class HotelController : Controller
{
    private readonly IHotelService _hotelService;

    public HotelController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    // Public catalog: anyone can browse and filter by city (no login required).
    [HttpGet]
    public async Task<IActionResult> Index(string? city)
    {
        var hotels = await _hotelService.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(city))
            hotels = hotels.Where(h => h.City.Contains(city, StringComparison.OrdinalIgnoreCase)).ToList();

        ViewBag.City = city;
        return View(hotels);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var hotel = await _hotelService.GetByIdAsync(id);
        if (hotel is null) return NotFound();
        return View(hotel);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Create() => View(new Hotel());

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Hotel hotel)
    {
        if (!ModelState.IsValid) return View(hotel);
        await _hotelService.CreateAsync(hotel);
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var hotel = await _hotelService.GetByIdAsync(id);
        if (hotel is null) return NotFound();
        return View(hotel);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Hotel hotel)
    {
        if (!ModelState.IsValid) return View(hotel);
        var updated = await _hotelService.UpdateAsync(id, hotel);
        if (updated is null) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _hotelService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
