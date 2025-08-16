using System.ComponentModel.DataAnnotations;
using EducationApp.Core.Common;

namespace EducationApp.Core.Entities;

public class StaffSubject : BaseEntity
{
    // relationships
    public int StaffId { get; set; }
    public Staff Staff { get; set; } = null!;

    // relationships
    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
}