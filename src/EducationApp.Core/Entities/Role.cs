using System.ComponentModel.DataAnnotations;
using EducationApp.Core.Common;

namespace EducationApp.Core.Entities;

public class Role : BaseEntity
{
    
    public string? Name { get; set; }

    // navigation properties
    public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
    public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
}
