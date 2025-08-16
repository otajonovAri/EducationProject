using EducationApp.Application.Auth;
using EducationApp.Application.Service.RoleServices;
using EducationApp.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController(IRoleService roleService) : ControllerBase
{
    [HttpPost]
	[PermissionAuthorize(Core.Permission.CreateRolePermission)]
    public async Task<IActionResult> Create(RoleDto dto)
	{
		await roleService.CreateAsync(dto);
		return Ok("Role created");
	}

	[HttpPost("{id:int}/permissions")]
	[PermissionAuthorize(Core.Permission.AssignPermissionToRolePermission)]
    public async Task<IActionResult> AssignPermission(int id, List<int> permissionIds)
	{
		foreach (var permissionId in permissionIds)
		{
			await roleService.AssignPermissionAsync(new RolePermissionDto
			{
				RoleId = id,
				PermissionId = permissionId
			});
		}

		return Ok("Permissions assigned to role");
	}
}
