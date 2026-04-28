namespace SportsReserves.DTOs;
using System.ComponentModel.DataAnnotations;

public class UserDto
{
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Document { get; set; }
    [Required]
    public required string Phone { get; set; }
    [Required]
    public required string Email { get; set; }
}


