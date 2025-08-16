using System.ComponentModel.DataAnnotations;

namespace EducationApp.Application.DTOs.StaffSubjectDto;

public class StaffSubjectResponseDto
{
    public int Id { get; set; }
    public int StaffId { get; set; }
    public int SubjectId { get; set; }
}