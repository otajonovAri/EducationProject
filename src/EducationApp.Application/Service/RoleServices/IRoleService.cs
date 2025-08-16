using EducationApp.Core.DTOs;

namespace EducationApp.Application.Service.RoleServices;

public interface IRoleService
{
	Task CreateAsync(RoleDto dto);
	Task AssignPermissionAsync(RolePermissionDto dto);
}
