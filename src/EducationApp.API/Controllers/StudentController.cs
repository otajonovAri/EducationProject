using EducationApp.Application.Auth;
using EducationApp.Application.DTOs.StudentDto;
using EducationApp.Application.Service.StudentServices;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController(IStudentService service) : ControllerBase
{
    [HttpGet("get-all-student")]
    [PermissionAuthorize(Core.Permission.GetAllStudentPermission)]  
    public async Task<IActionResult> GetAllAsync()
        => Ok(await service.GetAllAsync());

    [HttpGet("get-by-id-student/{id:int}")]
    [PermissionAuthorize(Core.Permission.GetByIdStudentPermission)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await service.GetByIdAsync(id);

        if (!result.Success)
            return NotFound(result.Message);

        return Ok(result);
    }

    [HttpGet("get-by-condition-count-active-students")]
    public async Task<IActionResult> GetByConditionCountActiveStudents()
    {
        var result = await service.GetByConditionCountActiveStudents();
        return Ok(result);
    }

    [HttpGet("get-by-condition-count-unpaid-students")]
    public async Task<IActionResult> GetByConditionCountUnpaidStudents()
    {
        var result = await service.GetByConditionCountUnpaidStudents();
        return Ok(result);
    }

    [HttpGet("get-by-condition-paid-students-on-this-month")]
    public async Task<IActionResult> GetByConditionPaidStudentsOnThisMonth()
    {
        var result = await service.GetByConditionPaidStudentsOnThisMonth();
        return Ok(result);
    }

    [HttpGet("get-by-condition-count-droppedout-students-active-group")]
    public async Task<IActionResult> GetByConditionCountDroppedOutStudentsActiveGroup()
    {
        var result = await service.GetByConditionCountDroppedOutStudentsActiveGroup();
        return Ok(result);
    }

    [HttpGet("get-by-condition-all-active-students")]
    public async Task<IActionResult> GetByConditionAllActiveStudents()
    {
        var result = await service.GetByConditionAllActiveStudents();
        return Ok(result);
    }

    [HttpGet("get-by-condition-all-inactive-students")]
    public async Task<IActionResult> GetByConditionAllInActiveStudents()
    {
        var result = await service.GetByConditionAllInActiveStudents();
        return Ok(result);
    }

    [HttpPut("get-by-condition-return-last-payment")]
    public async Task<IActionResult> GetByConditionReturnLastPayment([FromRoute]int studentId)
    {
        var result = await service.GetByConditionReturnLastPayment(studentId);
        return Ok(result);
    }

    [HttpPost("create-student")]
    [PermissionAuthorize(Core.Permission.CreateStudentPermission)]
    public async Task<IActionResult> CreateAsync([FromBody] StudentCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.CreateAsync(dto);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result);
        //return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Data }, result);
    }

    [HttpPut("update-student-by-id/{id:int}")]
    [PermissionAuthorize(Core.Permission.UpdateStudentPermission)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] StudentUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.UpdateAsync(id, dto);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpDelete("delete-student-by-id/{id:int}")]
    [PermissionAuthorize(Core.Permission.DeleteStudentPermission)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await service.DeleteAsync(id);

        if (!result.Success)
            return NotFound(result.Message);

        return Ok(result);
    }
}
