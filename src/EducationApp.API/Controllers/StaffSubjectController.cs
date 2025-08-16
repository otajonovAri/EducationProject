using EducationApp.Application.Auth;
using EducationApp.Application.DTOs.StaffSubjectDto;
using EducationApp.Application.Service.StaffSubjectServices;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StaffSubjectController(IStaffSubjectService service) : ControllerBase
{
    [HttpGet("get-all-staff-subject")]
    [PermissionAuthorize(Core.Permission.GetAllStaffSubjectPermission)]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await service.GetAllAsync());

    [HttpGet("get-by-id-staff-subject/{id:int}")]
    [PermissionAuthorize(Core.Permission.GetByIdStaffSubjectPermission)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await service.GetByIdAsync(id);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpPost("create-staff-subject")]
    [PermissionAuthorize(Core.Permission.CreateStaffSubjectPermission)]
    public async Task<IActionResult> CreateAsync([FromBody] StaffSubjectCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.CreateAsync(dto);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result);
        //return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Data }, result);
    }

    [HttpPut("update-staff-subject-by-id/{id:int}")]
    [PermissionAuthorize(Core.Permission.UpdateStaffSubjectPermission)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] StaffSubjectUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.UpdateAsync(id, dto);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpDelete("delete-staff-subject-by-id/{id:int}")]
    [PermissionAuthorize(Core.Permission.DeleteStaffSubjectPermission)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await service.DeleteAsync(id);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }
}
