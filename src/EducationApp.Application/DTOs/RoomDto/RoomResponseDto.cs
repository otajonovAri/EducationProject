namespace EducationApp.Application.DTOs.Room;

public class RoomResponseDto
{
    public int Id { get; set; }
    public string Number { get; set; } = null!;
    public int Capacity { get; set; }
    public string? Description { get; set; }
}