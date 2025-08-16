using System.ComponentModel.DataAnnotations;

namespace EducationApp.Application.DTOs.SubjectDto;

public class CreateSubjectDto
{
    [Required] public string Name { get; set; } = null!;
    public string? Description { get; set; }
}