using System.ComponentModel.DataAnnotations;
using EducationApp.Core.Enums;

namespace EducationApp.Application.DTOs.AttendanceDto;

public class AttendanceUpdateDto
{
    public bool? IsPresent { get; set; }
    public PreparednessLevel? Preparedness { get; set; }
    public ParticipationLevel? Participation { get; set; }
    [StringLength(500)] public string? Note { get; set; }
}