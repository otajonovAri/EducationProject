using System.ComponentModel.DataAnnotations;
using EducationApp.Core.Enums;

namespace EducationApp.Application.DTOs.StaffDto;

public class StaffCreateDto
{
    /* [Required] public Position Position { get; set; }
     [Range(0, 10000)] public decimal Salary { get; set; }
     [Required] public string FirstName { get; set; } = null!;
     [Required] public string LastName { get; set; } = null!;
     [Required] public string PhoneNumber { get; set; } = null!;
     [Required] public string Email { get; set; } = null!;
     [Required] public string Password { get; set; } = null!;
     public DateTime DateBirth { get; set; }
     public Gender Gender { get; set; }*/

    [Required][MinLength(3)] public string FirstName { get; set; } = null!;
    [Required][MinLength(3)] public string LastName { get; set; } = null!;
    [Required][Phone] public string PhoneNumber { get; set; } = null!;
    [Required][EmailAddress] public string Email { get; set; } = null!;
    [Required][StringLength(25, MinimumLength = 8)] public string Password { get; set; } = null!;
    [Required] public DateTime DateBirth { get; set; }
    [Required] public Gender Gender { get; set; }
    [Required] public Position Position { get; set; }
    [Required] public decimal Salary { get; set; }
}