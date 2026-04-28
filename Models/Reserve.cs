namespace SportsReserves.Models;

public class Reserve
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int SportId { get; set; }
    public Sport Sport { get; set; }
    
    public DateTime Date { get; set; }
    public TimeSpan HourStart { get; set; }
    public TimeSpan HourEnd { get; set; }
    
    public string State { get; set; } = "Programada";
    
    
    
    
}