using EducationApp.Application.Auth;
using EducationApp.Application.DTOs.GroupDto;
using EducationApp.Application.Service.GroupServices;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupController(IGroupService service) : ControllerBase
{
    [HttpGet("get-all-group")]
    [PermissionAuthorize(Core.Permission.GetAllGroupPermission)]
    public async Task<IActionResult> GetAll()
    {
        var result = await service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("get-by-id-group/{id:int}")]
    [PermissionAuthorize(Core.Permission.GetByIdGroupPermission)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpGet("get-by-condition-group-count")]
    public async Task<IActionResult> GetByConditionGroupCount()
    {
        var groupCount = await service.GetByConditionGroupCount();
        return Ok(groupCount);
    }

    [HttpGet("get-by-condition-all-active-groups")]
    public async Task<IActionResult> GetByConditionAllActiveGroups()
    {
        var result = await service.GetByConditionAllActiveGroups();
        return Ok(result);
    }

    [HttpGet("get-by-condition-all-no-active-groups")]
    public async Task<IActionResult> GetByConditionAllNoActiveGroups()
    {
        var result = await service.GetByConditionAllNoActiveGroups();
        return Ok(result);
    }

    [HttpPost("create-group")]
    [PermissionAuthorize(Core.Permission.CreateGroupPermission)]
    public async Task<IActionResult> Create([FromBody] GroupCreatedDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.CreateAsync(dto);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result);
        /*return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);*/
    }

    [HttpPut("update-group-by-id/{id:int}")]
    [PermissionAuthorize(Core.Permission.UpdateGroupPermission)]
    public async Task<IActionResult> Update(int id, [FromBody] GroupUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.UpdateAsync(id, dto);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpDelete("delete-group-by-id/{id:int}")]
    [PermissionAuthorize(Core.Permission.DeleteGroupPermission)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await service.DeleteAsync(id);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }
}
