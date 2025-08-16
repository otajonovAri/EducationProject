using System.ComponentModel.DataAnnotations;
using EducationApp.Core.Common;

namespace EducationApp.Core.Entities;

public class Room : BaseEntity
{
    public string? Number { get; set; }
    public int Capacity { get; set; }
    public string? Description { get; set; }

    // Navigation properties
    public ICollection<GroupSubject> GroupSubjects { get; set; } = new HashSet<GroupSubject>();
}