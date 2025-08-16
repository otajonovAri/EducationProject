using EducationApp.Application.Auth;
using EducationApp.Application.DTOs.GroupSubjectDto;
using EducationApp.Application.Service.GroupSubjectServices;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupSubjectController(IGroupSubjectService service) : ControllerBase
{
    [HttpGet("get-all-group-subject")]
    [PermissionAuthorize(Core.Permission.GetAllGroupSubjectPermission)]
    public async Task<IActionResult> GetAll() => Ok(await service.GetAllAsync());

    [HttpGet("get-by-id-group-subject/{id:int}")]
    [PermissionAuthorize(Core.Permission.GetByIdGroupSubjectPermission)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpPost("create-group-subject")]
    [PermissionAuthorize(Core.Permission.CreateGroupSubjectPermission)]
    public async Task<IActionResult> Create([FromBody] GroupSubjectCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.CreateAsync(dto);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result);
        //return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
    }

    [HttpPut("update-group-subject-by-id/{id:int}")]
    [PermissionAuthorize(Core.Permission.UpdateGroupSubjectPermission)]
    public async Task<IActionResult> Update(int id, [FromBody] GroupSubjectUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.UpdateAsync(id, dto);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpDelete("delete-group-subject-by-id/{id:int}")]
    [PermissionAuthorize(Core.Permission.DeleteGroupSubjectPermission)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await service.DeleteAsync(id);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }
}
