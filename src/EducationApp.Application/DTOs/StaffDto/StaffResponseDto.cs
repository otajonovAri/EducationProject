using EducationApp.Core.Enums;

namespace EducationApp.Application.DTOs.StaffDto;

public class StaffResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime DateBirth { get; set; }
    public Gender Gender { get; set; }
    public Position Position { get; set; }
    public decimal Salary { get; set; }
}