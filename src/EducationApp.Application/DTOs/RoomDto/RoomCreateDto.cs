using System.ComponentModel.DataAnnotations;

namespace EducationApp.Application.DTOs.Room;

public class RoomCreateDto
{
    [Required] public string Number { get; set; } = null!;
    [Required] public int Capacity { get; set; }
    public string? Description { get; set; }
}