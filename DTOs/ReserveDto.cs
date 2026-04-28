namespace SportsReserves.DTOs;

public class ReserveDto
{
    public int UserId { get; set; }
    public int SportId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan HourStart { get; set; }
    public TimeSpan HourEnd { get; set; }
    
    public string State { get; set; } = "Programada";
}