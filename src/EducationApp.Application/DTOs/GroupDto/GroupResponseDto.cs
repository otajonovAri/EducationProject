namespace EducationApp.Application.DTOs.GroupDto;

public class GroupResponseDto
{
    /*public int Id { get; init; }
    public string? Name { get; init; }
    public int StaffId { get; init; }
    public string? StaffName { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string? Description { get; init; }*/

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int StaffId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }
}