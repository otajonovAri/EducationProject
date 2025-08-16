using System.ComponentModel.DataAnnotations;
using EducationApp.Core.Common;

namespace EducationApp.Core.Entities;

public class ScheduleSlot : BaseEntity
{
    public DayOfWeek Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;
}