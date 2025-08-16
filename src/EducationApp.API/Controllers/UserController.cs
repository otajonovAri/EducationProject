using EducationApp.Core;
using EducationApp.Application.DTOs.UserDto;
using EducationApp.Application.Service.UserServices;
using Microsoft.AspNetCore.Mvc;
using EducationApp.Application.Auth;

namespace EducationApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService service) : ControllerBase
{
    [HttpGet("get-all-user")]
    [PermissionAuthorize(Permission.GetAllUserPermission)]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await service.GetAllAsync());

    [HttpGet("get-by-id-user/{id:int}")]
    [PermissionAuthorize(Permission.GetByIdUserPermission)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await service.GetByIdAsync(id);

        if (!result.Success)
            return NotFound(result.Message);

        return Ok(result);
    }

    [HttpPost("create-user")]
    [PermissionAuthorize(Permission.CreateUserPermission)]
    public async Task<IActionResult> CreateAsync([FromBody] UserCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await service.CreateAsync(dto);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPut("update-user-by-id/{id:int}")]
    [PermissionAuthorize(Permission.UpdateUserPermission)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UserUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await service.UpdateAsync(id, dto);
        if (!result.Success)
            return NotFound(result.Message);

        return Ok(result);
    }

    [HttpDelete("delete-user-by-id/{id:int}")]
    [PermissionAuthorize(Permission.DeleteUserPermission)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await service.DeleteAsync(id);

        if (!result.Success)
            return NotFound(result.Message);

        return Ok(result);
    }
}
