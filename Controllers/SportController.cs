using Microsoft.AspNetCore.Mvc;
using SportsReserves.DTOs;
using SportsReserves.Interfaces;

namespace SportsReserves.Controllers;

public class SportController : Controller
{
    private readonly ISportService _sportService;

    public SportController(ISportService sportService)
    {
        _sportService = sportService;
    }

    public async Task<IActionResult> Index(string typeSport)
    {
        if (string.IsNullOrEmpty(typeSport))
        {
            var sports = await _sportService.GetAll();
            return View(sports);
        }

        var filtered = await _sportService.GetSportByType(typeSport);
        return View(filtered);
    }
  
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(SportDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }
        try
        {
            await _sportService.Create(dto);
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            ViewBag.Error = e.Message;
            return View(dto);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var sport = await _sportService.GetSportById(id);
        if (sport == null)
            return NotFound();

        var dto = new SportDto()
        {
            TypeSport = sport.TypeSport,
            Capacity = sport.Capacity
        };
        
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, SportDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);
        try
        {
            await _sportService.Edit(id, dto);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(dto);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _sportService.GetSportById(id);
        if (user == null)
            return NotFound();
        return View(user);
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> Destroy(int id, SportDto dto)
    {
        try
        {
            await _sportService.Delete(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

   
    
}