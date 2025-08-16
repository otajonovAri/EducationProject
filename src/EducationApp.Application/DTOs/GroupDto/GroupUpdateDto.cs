using System.ComponentModel.DataAnnotations;

namespace EducationApp.Application.DTOs.GroupDto;

public class GroupUpdateDto
{
    /* [StringLength(100)] public string? Name { get; set; }
     public DateTime? StartDate { get; set; }
     public DateTime? EndDate { get; set; }
     [StringLength(500)] public string? Description { get; set; }*/

    public string? Name { get; set; }
    public int? StaffId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
}