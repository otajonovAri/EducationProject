using System.ComponentModel.DataAnnotations;
using EducationApp.Core.Common;

namespace EducationApp.Core.Entities;

public class GroupSubject : BaseEntity
{
    // relationships
    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;

    // relationships
    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    public ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();

    // relationships
    public int StaffId { get; set; }
    public Staff Staff { get; set; } = null!;

    // relationships
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;

    public DateTime SessionDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }   
}
