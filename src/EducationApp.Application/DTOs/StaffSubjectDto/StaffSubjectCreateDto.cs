using System.ComponentModel.DataAnnotations;

namespace EducationApp.Application.DTOs.StaffSubjectDto;

public class StaffSubjectCreateDto
{
    [Required] public int StaffId { get; set; }
    [Required] public int SubjectId { get; set; }
}