namespace SportsReserves.Models;

public class Sport
{
    public int Id { get; set; }
    public string TypeSport { get; set; } 
    public int Capacity { get; set; }
    
    public List<Reserve> Reserves { get; set; } = new();
}