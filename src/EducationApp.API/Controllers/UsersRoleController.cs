using EducationApp.Application.Auth;
using EducationApp.Application.Service.UserRoleServices;
using EducationApp.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersRoleController : ControllerBase
{
    private readonly IUserRoleService _userRoleService;

    public UsersRoleController(IUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    [HttpPost("{id}/roles")]
    [PermissionAuthorize(Core.Permission.AssignRolesToUserPermission)]
    public async Task<IActionResult> AssignRoles(int id, List<int> roleIds)
    {
        foreach (var roleId in roleIds)
        {
            await _userRoleService.AssignAsync(new UserRoleDto
            {
                UserId = id,
                RoleId = roleId
            });
        }
        return Ok("Roles assigned to user");
    }
    /*[HttpPost("{id}/roles")]
	public async Task<IActionResult> AssignRoles(int id, List<int> roleIds)
	private readonly IUserRoleService _userRoleService;

	public UsersController(IUserRoleService userRoleService)
	{
		_userRoleService = userRoleService;
	}

	[HttpPost("{id}/roles")]
	[PermissionAuthorize(Core.Permission.AssignRolesToUserPermission)]
	public async Task<IActionResult> AssignRoles(int id, List<int> roleIds)
	{
		foreach (var roleId in roleIds)
		{
			await userRoleService.AssignAsync(new UserRoleDto
			{
				UserId = id,
				RoleId = roleId
			});
		}
		return Ok("Roles assigned to user");
	}*/
}
