using System.ComponentModel.DataAnnotations;
using EducationApp.Core.Enums;

namespace EducationApp.Application.DTOs.UserDto;

public class UserUpdateDto
{
    [MinLength(3)][MaxLength(100)] public string? FirstName { get; set; }
    [MinLength(3)][MaxLength(100)] public string? LastName { get; set; }
    [MinLength(9)][MaxLength(15)][Phone] public string? PhoneNumber { get; set; }
    public DateTime? DateBirth { get; set; }
    public Gender? Gender { get; set; }
}