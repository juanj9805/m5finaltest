using Final.Domain.Data;
using Final.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final.Web.Controllers;

[Authorize]
public class WishlistController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public WishlistController(AppDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var client = await GetOrCreateClientAsync();
        var hotels = await _context.WhishLists
            .Where(w => w.ClientId == client.Id)
            .Include(w => w.Hotel)
            .Select(w => w.Hotel!)
            .ToListAsync();

        return View(hotels);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int hotelId)
    {
        var client = await GetOrCreateClientAsync();

        var exists = await _context.WhishLists
            .AnyAsync(w => w.ClientId == client.Id && w.HotelId == hotelId);

        if (!exists)
        {
            _context.WhishLists.Add(new WhishList { ClientId = client.Id, HotelId = hotelId });
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index", "Hotel");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int hotelId)
    {
        var client = await GetOrCreateClientAsync();

        var fav = await _context.WhishLists
            .FirstOrDefaultAsync(w => w.ClientId == client.Id && w.HotelId == hotelId);

        if (fav is not null)
        {
            _context.WhishLists.Remove(fav);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<Client> GetOrCreateClientAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.IdentityUserId == user!.Id);

        if (client is null)
        {
            client = new Client
            {
                IdentityUserId = user!.Id,
                Name = user.UserName ?? user.Email ?? "Guest",
                Email = user.Email ?? string.Empty
            };
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        return client;
    }
}
