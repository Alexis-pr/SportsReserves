using Microsoft.EntityFrameworkCore;
using SportsReserves.Interfaces;
using SportsReserves.Models;
using SportsReserves.Data;
using SportsReserves.DTOs;


namespace SportsReserves.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private IUserService _userServiceImplementation;


    public UserService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserById(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
            throw new Exception("No se encontró el propietario");
        
        return (user);
    }

    public async Task<User> Create(UserDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Document == dto.Document))
            throw new Exception("El Documento ya existe");
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            throw new Exception("El Correo del usuario ya existe");

        var user = new User
        {
            Name = dto.Name,
            Document = dto.Document,
            Email = dto.Email,
            Phone = dto.Phone
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        return user;
        
    }

    public async Task<User> Edit(int id, UserDto dto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            throw new Exception("No se encontró el propietario");
        
        if (await _context.Users.AnyAsync(u => u.Document == dto.Document && u.Id != id))
            throw new Exception("El documento ya existe");
        
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email && u.Id != id))
            throw new Exception("El correo ya existe");
        
        user.Document = dto.Document;
        user.Name = dto.Name;
        user.Phone = dto.Phone;
        user.Email = dto.Email;

        await _context.SaveChangesAsync();
        
        return user;
    }

    public async Task<User> Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            throw new Exception("No se encontró el propietario");
        
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }
}
