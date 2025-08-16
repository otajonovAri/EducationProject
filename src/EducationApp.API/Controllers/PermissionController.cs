using EducationApp.Application.Auth;
using EducationApp.Application.Service.PermissionServices;
using EducationApp.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PermissionController(IPermissionService permissionService) : ControllerBase
{
    [HttpPost]
	[PermissionAuthorize(Core.Permission.CreatePermissionPermission)]
    public async Task<IActionResult> Create(PermissionDto dto)
	{
		await permissionService.CreateAsync(dto);
		return Ok("Permission created");
	}

	[HttpGet("get-all-permissions")]
	[PermissionAuthorize(Core.Permission.GetAllPermissionPermission)]
    public IActionResult GetAllPermissions()
	{
		var permissions = permissionService.GetAll();
		return Ok(permissions);
    }
}
