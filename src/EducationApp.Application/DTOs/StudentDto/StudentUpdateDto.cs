using EducationApp.Core.Enums;

namespace EducationApp.Application.DTOs.StudentDto;

public class StudentUpdateDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTime? DateBirth { get; set; }
    public Gender? Gender { get; set; }
    public Status? Status { get; set; }
    public DateTime? JoinDate { get; set; }
}