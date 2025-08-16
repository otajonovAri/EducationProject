using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EducationApp.Core.Enums;

namespace EducationApp.Application.DTOs.StudentDto;

public class StudentCreateDto
{
    [Required][MinLength(3)] public string FirstName { get; set; } = null!;
    [Required][MinLength(3)] public string LastName { get; set; } = null!;
    [Required][Phone] public string PhoneNumber { get; set; } = null!;
    [Required][EmailAddress] public string Email { get; set; } = null!;
    [Required][StringLength(25, MinimumLength = 8)] public string Password { get; set; } = null!;
    [Required] public DateTime DateBirth { get; set; }
    [Required] public Gender Gender { get; set; }
    [Required] public Status Status { get; set; }
    [Required] public DateTime JoinDate { get; set; }
}