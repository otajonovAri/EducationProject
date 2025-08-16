using System.ComponentModel.DataAnnotations;

namespace EducationApp.Application.DTOs.GroupDto;

public class GroupCreatedDto
{
    /* [Required][StringLength(100)] public string? Name { get; set; }
     [Required] public int StaffId { get; set; }
     [Required] public DateTime StartDate { get; set; }
     [Required] public DateTime EndDate { get; set; }
     [StringLength(500)] public string? Description { get; set; }*/

    [Required] public string Name { get; set; } = null!;
    [Required] public int StaffId { get; set; }
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
    public string? Description { get; set; }
}