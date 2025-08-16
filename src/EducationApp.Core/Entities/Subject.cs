using EducationApp.Core.Common;

namespace EducationApp.Core.Entities;

public class Subject : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // navigation properties
    public ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();
    public ICollection<GroupSubject> GroupSubjects { get; set; } = new HashSet<GroupSubject>();
    public ICollection<StaffSubject> StaffSubjects { get; set; } = new HashSet<StaffSubject>();
}
