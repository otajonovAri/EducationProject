using EducationApp.Core.Enums;

namespace EducationApp.Application.DTOs.AttendanceDto;

public class AttendanceResponseDto
{
    /* public int Id { get; set; }
     public int StudentId { get; set; }
     public string? StudentName { get; set; }
     public int GroupSubjectId { get; set; }
     public string? SessionInfo { get; set; }
     public DateTime AttendanceDate { get; set; }
     public bool IsPresent { get; set; }
     public PreparednessLevel? Preparedness { get; set; }
     public ParticipationLevel? Participation { get; set; }
     public string? Note { get; set; }*/

    public int Id { get; set; }
    public int StudentId { get; set; }
    public int GroupSubjectId { get; set; }
    public DateTime AttendanceDate { get; set; }
    public bool IsPresent { get; set; }
    public PreparednessLevel? Preparedness { get; set; }
    public ParticipationLevel? Participation { get; set; }
    public string? Note { get; set; }
}
