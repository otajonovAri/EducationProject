namespace EducationApp.Application.DTOs.ScheduleSlotDto;

public class ScheduleSlotResponseDto
{
    public int Id { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int GroupId { get; set; }
}