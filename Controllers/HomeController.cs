using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsReserves.Data;


namespace SimulacroCS.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.TotalPropietarios = await _context.Users.CountAsync();
        ViewBag.TotalMascotas = await _context.Sports.CountAsync();
        ViewBag.TotalReserves = await _context.Reserves.CountAsync();
      

        ViewBag.CitasHoy = await _context.Reserves
            .Where(c => c.Date.Date == DateTime.Now.Date)
            .CountAsync();

        return View();
    }
}