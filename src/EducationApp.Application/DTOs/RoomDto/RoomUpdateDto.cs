using System.ComponentModel.DataAnnotations;

namespace EducationApp.Application.DTOs.Room;

public class RoomUpdateDto
{
    public string? Number { get; set; }
    public int? Capacity { get; set; }
    public string? Description { get; set; }
}