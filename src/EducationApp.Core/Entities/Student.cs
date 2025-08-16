using System.ComponentModel.DataAnnotations;
using EducationApp.Core.Common;
using EducationApp.Core.Enums;

namespace EducationApp.Core.Entities;

public class Student : User
{
    public Status Status { get; set; }
    public DateTime JoinDate { get; set; }

    // navigation properties
    public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
    public ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();
}
