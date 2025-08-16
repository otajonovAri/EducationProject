using EducationApp.Core.Enums;

namespace EducationApp.Application.DTOs.UserDto;

public class UserResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsVerified { get; set; }
    public DateTime DateBirth { get; set; }
    public Gender Gender { get; set; }
    public DateTime CreatedAt { get; set; }
}