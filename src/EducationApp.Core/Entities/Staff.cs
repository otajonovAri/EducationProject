using EducationApp.Core.Enums;

namespace EducationApp.Core.Entities;

public class Staff : User
{
    public Position Position { get; set; } = Position.Other;
    public StaffStatus StaffStatus { get; set; }
    public Decimal Salary { get; set; }

    // navigation properties
    public ICollection<Group> Groups { get; set; } = new HashSet<Group>();
    public ICollection<GroupSubject> GroupSubjects { get; set; } = new HashSet<GroupSubject>();
    public ICollection<StaffSubject> StaffSubjects { get; set; } = new HashSet<StaffSubject>();
}
