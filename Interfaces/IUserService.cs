using SportsReserves.DTOs;
using SportsReserves.Models;

namespace SportsReserves.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAll();
    Task<User> GetUserById(int id);
    //Task<User> GetUserByEmail(string email);
   
    Task<User> Create(UserDto dto);
    
    Task<User> Edit(int id, UserDto dto);
    Task<User> Delete(int id);
    
}