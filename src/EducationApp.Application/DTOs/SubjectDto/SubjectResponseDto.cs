namespace EducationApp.Application.DTOs.SubjectDto;

public class SubjectResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}