using SportsReserves.DTOs;
using SportsReserves.Models;

namespace SportsReserves.Interfaces;


public interface ISportService
{
    Task<List<Sport>> GetAll();
    Task<List<Sport>> GetSportByType(string TypeSport); // seria el filtro
    
    Task<Sport> GetSportById(int id);
    Task<Sport> Edit(int id, SportDto dto);
   
    Task<Sport> Create(SportDto dto);
   
    Task<Sport> Delete(int id);
}