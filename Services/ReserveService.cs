using Microsoft.EntityFrameworkCore;
using SportsReserves.Data;
using SportsReserves.DTOs;
using SportsReserves.Interfaces;
using SportsReserves.Models;
namespace SportsReserves.Services;

public class ReserveService : IReserveService
{
    private readonly AppDbContext _context;

    public ReserveService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Reserve>> GetAllReserves()
    {
        return await _context.Reserves
            .Include(r => r.Sport)
            .Include(r => r.User)
            .ToListAsync();
    }

    public async Task<Reserve> GetReserveById(int reserveId)
    {
        return await _context.Reserves
            .Include(r => r.Sport)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == reserveId);
        
    }

    public async Task Create(ReserveDto dto)
    {
        if (dto.HourEnd <= dto.HourStart)
            throw new Exception("La hora fin debe ser mayor a la hora inicio");

        if (dto.Date < DateTime.Today)
            throw new Exception("No se permiten fechas pasadas");

        var conflict = await _context.Reserves.AnyAsync(r =>
            r.SportId == dto.SportId &&
            r.Date == dto.Date &&
            (dto.HourStart < r.HourEnd && dto.HourEnd > r.HourStart)
        );

        if (conflict)
            throw new Exception("Ya existe una reserva en ese horario");

        var reserve = new Reserve
        {
            SportId = dto.SportId,
            UserId = dto.UserId,
            Date = dto.Date,
            HourStart = dto.HourStart,
            HourEnd = dto.HourEnd,
            State = "Programada"
        };

        _context.Reserves.Add(reserve);
        await _context.SaveChangesAsync();
    }
    
    public async Task<string> ChangeState(int id, ReserveDto dto)
    {
        var reserve = await _context.Reserves
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);
            
        if (reserve == null)
            throw new Exception("No se permiten el registro");
        
        reserve.State = dto.State;
        
        await _context.SaveChangesAsync();
        
        return dto.State switch
        {
            "Cancelada" => "Reserva cancelada ❌",
            "Atendida" => "Reserva atendida ✔",
            _ => "Estado actualizado"
        };
    }

    /*
     
    public async Task<string> ChangeState(int id, ReserveDto dto)
    {
        var reserve = await _context.Reserves
            .FirstOrDefaultAsync(r => r.Id == id);

        if (reserve == null)
            throw new Exception("Reserva no encontrada");

        reserve.State = dto.State;

        await _context.SaveChangesAsync();

        return dto.State switch
        {
            "Cancelada" => "Reserva cancelada ❌",
            "Atendida" => "Reserva atendida ✔",
            _ => "Estado actualizado"
        };
    }
    */

    public async Task<Reserve> Delete(int id)
    {
        var reserve = await _context.Reserves.FindAsync(id);

        if (reserve == null)
            throw new Exception("No se encontro ninguna reserva con este ID");
        
        _context.Reserves.Remove(reserve);
        await  _context.SaveChangesAsync();
        return reserve;
    }
        
}