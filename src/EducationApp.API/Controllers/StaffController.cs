using EducationApp.Application.Auth;
using EducationApp.Application.DTOs.StaffDto;
using EducationApp.Application.Service.StaffServices;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StaffController(IStaffService service) : ControllerBase
{
    [HttpGet("get-all-staff")]
    [PermissionAuthorize(Core.Permission.GetAllStaffPermission)]
    public async Task<IActionResult> GetAll() => Ok(await service.GetAllAsync());

    [HttpGet("get-by-id-staff/{id:int}")]
    [PermissionAuthorize(Core.Permission.GetByIdStaffPermission)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpGet("get-by-condition-staff-count")]
    public async Task<IActionResult> GetByConditionStaffCount()
    {
        var result = await service.GetByConditionStaffCount();
        return Ok(result);
    }

    [HttpGet("get-by-condition-all-active-staff")]
    public async Task<IActionResult> GetByConditionAllActiveStaff()
    {
        var result = await service.GetByConditionAllActiveStaff();
        return Ok(result);
    }

    [HttpGet("get-by-condition-all-teachers")]
    public async Task<IActionResult> GetByConditionAllTeachers()
    {
        var result = await service.GetByConditionAllTeachers();
        return Ok(result);
    }

    [HttpGet("get-by-condition-all-other-staff")]
    public async Task<IActionResult> GetByConditionAllOtherStaff()
    {
        var result = await service.GetByConditionAllOtherStaff();
        return Ok(result);
    }

    [HttpPost("create-staff")]
    [PermissionAuthorize(Core.Permission.CreateStaffPermission)]
    public async Task<IActionResult> Create([FromBody] StaffCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.CreateAsync(dto);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result);
        //return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
    }

    [HttpPut("update-staff-by-id/{id:int}")]
    [PermissionAuthorize(Core.Permission.UpdateStaffPermission)]
    public async Task<IActionResult> Update(int id, [FromBody] StaffUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.UpdateAsync(id, dto);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpDelete("delete-staff-by-id/{id:int}")]
    [PermissionAuthorize(Core.Permission.DeleteStaffPermission)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await service.DeleteAsync(id);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }
}
