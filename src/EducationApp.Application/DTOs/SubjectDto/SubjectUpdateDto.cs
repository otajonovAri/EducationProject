using System.ComponentModel.DataAnnotations;

namespace EducationApp.Application.DTOs.SubjectDto;

public class SubjectUpdateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}