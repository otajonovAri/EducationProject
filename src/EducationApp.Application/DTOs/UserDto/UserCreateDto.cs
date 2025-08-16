using EducationApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace EducationApp.Application.DTOs.UserDto;

public record UserCreateDto
{
    [Required]  [MinLength(3)] [MaxLength(100)] public string FirstName { get; set; } = null!;
    [Required] [MinLength(3)][MaxLength(100)] public string LastName { get; set; } = null!;
    [Required] [MinLength(9)] [MaxLength(30)] [Phone] public string PhoneNumber { get; set; } = null!;
    [Required] [EmailAddress] public string Email { get; set; } = null!;
    [Required] [MinLength(8)] public string Password { get; set; } = null!;
    [Required] public Gender Gender { get; set; }
}