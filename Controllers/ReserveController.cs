using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsReserves.DTOs;
using SportsReserves.Interfaces;

namespace SportsReserves.Controllers
{
    public class ReserveController : Controller
    {
        private readonly IReserveService _reserve;
        private readonly ISportService _sport;
        private readonly IUserService _user;

        public ReserveController(
            IReserveService reserveService,
            ISportService sportService,
            IUserService userService)
        {
            _reserve = reserveService;
            _sport = sportService;
            _user = userService;
        }

        // ---------------- INDEX ----------------
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reserves = await _reserve.GetAllReserves();
            return View(reserves);
        }

        // ---------------- GET RESERVE BY ID ----------------
        [HttpGet]
        public async Task<IActionResult> GetReserveById(int id)
        {
            var reserve = await _reserve.GetReserveById(id);
            if (reserve == null)
                return NotFound();
            
            return View(reserve);
        }

        // ---------------- CREATE (GET) ----------------
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadSelectLists();
            return View();
        }

        // ---------------- CREATE (POST) ----------------
        [HttpPost]
        public async Task<IActionResult> Create(ReserveDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadSelectLists();
                return View(dto);
            }

            try
            {
                await _reserve.Create(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                await LoadSelectLists();
                return View(dto);
            }
        }

        // ---------------- CHANGE STATE ----------------
        [HttpPost]
        public async Task<IActionResult> ChangeState(int id, ReserveDto dto)
        {
            var message = await _reserve.ChangeState(id, dto);
            TempData["Message"] = message;
            return RedirectToAction("Index");
        }

        // ---------------- DELETE ----------------
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _reserve.Delete(id);
                TempData["Message"] = "Reserva eliminada correctamente ✔";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        // ---------------- HELPER: CARGA DE SELECTLIST ----------------
        private async Task LoadSelectLists()
        {
            var users = await _user.GetAll();
            var sports = await _sport.GetAll();

            ViewBag.Users = new SelectList(users, "Id", "Name");
            ViewBag.Sports = new SelectList(sports, "Id", "TypeSport");
        }
    }
}