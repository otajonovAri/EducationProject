using System.ComponentModel.DataAnnotations;
using EducationApp.Core.Common;

namespace EducationApp.Core.Entities;

public class Permission : BaseEntity
{
    
    public string? Name { get; set; }

    public string? Description { get; set; }

    // Navigation properties
    public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
}
