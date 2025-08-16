using System.ComponentModel.DataAnnotations;

namespace EducationApp.Application.DTOs.ScheduleSlotDto;

public class ScheduleSlotCreateDto
{
    [Required] public DayOfWeek Day { get; set; }
    [Required] public TimeSpan StartTime { get; set; }
    [Required] public TimeSpan EndTime { get; set; }
    [Required] public int GroupId { get; set; }
}