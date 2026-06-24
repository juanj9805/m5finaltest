using Final.Domain.Interfaces;
using Final.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final.Web.Controllers;

[Authorize]
public class ClientController : Controller
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var clients = await _clientService.GetAllAsync();
        return View(clients);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client is null) return NotFound();
        return View(client);
    }

    [HttpGet]
    public IActionResult Create() => View(new Client());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Client client)
    {
        if (!ModelState.IsValid) return View(client);
        await _clientService.CreateAsync(client);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client is null) return NotFound();
        return View(client);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Client client)
    {
        if (!ModelState.IsValid) return View(client);
        var updated = await _clientService.UpdateAsync(id, client);
        if (updated is null) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _clientService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
