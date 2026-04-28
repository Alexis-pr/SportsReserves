using SportsReserves.DTOs;
using SportsReserves.Models;

namespace SportsReserves.Interfaces;

public interface IReserveService
{
    Task<List<Reserve>> GetAllReserves();
    Task<Reserve> GetReserveById(int reserveId);
    Task Create(ReserveDto dto);
    Task<string> ChangeState(int id, ReserveDto dto);
    Task<Reserve> Delete(int id);
}