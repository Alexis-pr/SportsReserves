using Microsoft.EntityFrameworkCore;
using SportsReserves.Models;

namespace SportsReserves.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Sport> Sports { get; set; }
    public DbSet<Reserve> Reserves { get; set; }
    
}