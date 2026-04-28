using Microsoft.AspNetCore.Mvc;
using SportsReserves.DTOs;
using SportsReserves.Interfaces;

namespace SportsReserves.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    public async Task<IActionResult> Index()
    {
        var Users = await _userService.GetAll();
        return View(Users);
    }   
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            await _userService.Create(dto);
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
        var user = await _userService.GetUserById(id);
        if (user == null)
            return NotFound();
        var dto = new UserDto
        {
            Name = user.Name,
            Document = user.Document,
            Phone = user.Phone,
            Email = user.Email
        };
        
        return View(dto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id, UserDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);
        try
        {
            await _userService.Edit(id, dto);
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
        var user = await _userService.GetUserById(id);
        if (user == null)
            return NotFound();
        return View(user);
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> Destroy(int id)
    {
        try
        {
            await _userService.Delete(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Index");
        }
    }
    
    
    
    
}
