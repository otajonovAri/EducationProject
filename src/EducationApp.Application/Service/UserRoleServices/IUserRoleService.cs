using EducationApp.Core.DTOs;

namespace EducationApp.Application.Service.UserRoleServices;

public  interface IUserRoleService
{
	Task AssignAsync(UserRoleDto userRoleDto);
	Task CreateAsync(PermissionDto dto);
}
