using EducationApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace EducationApp.Application.DTOs.AttendanceDto;

public class AttendanceCreateDto
{
    [Required] public int StudentId { get; set; }
    [Required] public int GroupSubjectId { get; set; }
    [Required] public int GroupId { get; set; }
    [Required] public DateTime AttendanceDate { get; set; }
    [Required] public bool IsPresent { get; set; }
    public PreparednessLevel? Preparedness { get; set; }
    public ParticipationLevel? Participation { get; set; }
    public string? Note { get; set; }
}