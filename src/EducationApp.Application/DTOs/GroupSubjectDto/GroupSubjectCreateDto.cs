using System.ComponentModel.DataAnnotations;

namespace EducationApp.Application.DTOs.GroupSubjectDto;

public class GroupSubjectCreateDto
{
    /* [Required] public int GroupId { get; set; }
     [Required] public int SubjectId { get; set; }
     [Required] public int StaffId { get; set; }
     [Required] public int RoomId { get; set; }
     [Required] public DateTime SessionDate { get; set; }
     [Required] public TimeSpan StartTime { get; set; }
     [Required] public TimeSpan EndTime { get; set; }*/
    [Required] public int GroupId { get; set; }
    [Required] public int SubjectId { get; set; }
    [Required] public int StaffId { get; set; }
    [Required] public int RoomId { get; set; }
    [Required] public DateTime SessionDate { get; set; }
    [Required] public TimeSpan StartTime { get; set; }
    [Required] public TimeSpan EndTime { get; set; }
}