using System.ComponentModel.DataAnnotations;
using EducationApp.Core.Common;

namespace EducationApp.Core.Entities;

public class UserRole : BaseEntity
{
    // relationships
    public int UserId { get; set; }
    public User User { get; set; } = null!;

     // relationships
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
}
