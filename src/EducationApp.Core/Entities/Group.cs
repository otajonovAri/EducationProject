using EducationApp.Core.Common;
using EducationApp.Core.Enums;

namespace EducationApp.Core.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; } = null!;

    // relationships
    public int StaffId { get; set; }
    public Staff Staff { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string? Description { get; set; }

    public GroupStatus GroupStatus { get; set; }
    public GroupDays GroupDays { get; set; }

    // navigation properties
    public ICollection<GroupSubject> GroupSubjects { get; set; } = new HashSet<GroupSubject>();
    public ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();
    public ICollection<ScheduleSlot> ScheduleSlots { get; set; } = new HashSet<ScheduleSlot>();
}
