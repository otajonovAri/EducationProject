using System.ComponentModel.DataAnnotations;
using EducationApp.Core.Common;

namespace EducationApp.Core.Entities;

public class RolePermission : BaseEntity
{
     // relationships
     public int RoleId { get; set; }
     public Role Role { get; set; } = null!;

     // relationships
     public int PermissionId { get; set; }
     public Permission Permission { get; set; } = null!;
}
