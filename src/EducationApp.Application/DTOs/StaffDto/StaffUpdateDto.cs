using System.ComponentModel.DataAnnotations;
using EducationApp.Core.Enums;

namespace EducationApp.Application.DTOs.StaffDto;

public class StaffUpdateDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTime? DateBirth { get; set; }
    public Gender? Gender { get; set; }
    public Position? Position { get; set; }
    public decimal? Salary { get; set; }
}