using Microsoft.EntityFrameworkCore;
using SportsReserves.Interfaces;
using SportsReserves.Models;
using SportsReserves.Data;
using SportsReserves.DTOs;

namespace SportsReserves.Services;

public class SportService  : ISportService
{
    private readonly AppDbContext _context;
    public SportService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<Sport>> GetAll()
    {
        return await _context.Sports.ToListAsync();
    }

    public async Task<List<Sport>> GetSportByType(string typeSport)
    {
        return await _context.Sports
            .Where(s => s.TypeSport == typeSport)
            .ToListAsync();
    }


    public async Task<Sport> GetSportById(int id)
    {
        var sport = _context.Sports.Find(id);
        if (sport == null)
            throw new Exception("No se encontró el propietario");
        
        return (sport);
    }


    public async Task<Sport> Create(SportDto dto)
    {
        if (await _context.Sports.AnyAsync(s => s.TypeSport == dto.TypeSport))
            throw new Exception("El tipo de deporte ya existe");

        var sport = new Sport
        {
            TypeSport = dto.TypeSport,
            Capacity = dto.Capacity
        };

        _context.Sports.Add(sport);
        await _context.SaveChangesAsync();

        return sport;
    }

    public async Task<Sport> Edit(int id, SportDto dto)
    {
        var sport = await _context.Sports.FindAsync(id);
        if (sport == null)
            throw new Exception("No se encontró el iD del tipo de deporte");
        
        if (await _context.Sports.AnyAsync(s => s.TypeSport == dto.TypeSport && s.Id != id))
            throw new Exception("El tipo de deporte ya existe");
        
        sport.TypeSport = dto.TypeSport;
        sport.Capacity = dto.Capacity;
        _context.Sports.Update(sport);
        await _context.SaveChangesAsync();
        return sport;
    }
    
    public async Task<Sport> Delete(int id)
    {
        var sport = await _context.Sports.FindAsync(id);

        if (sport == null)
            throw new Exception("No se encontro ningun deporte con este ID");
        _context.Sports.Remove(sport);
        await  _context.SaveChangesAsync();
        return sport;
    }
}