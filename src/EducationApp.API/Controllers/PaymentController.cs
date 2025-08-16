using EducationApp.Application.Auth;
using EducationApp.Application.DTOs.PaymentDto;
using EducationApp.Application.Service.PaymentServices;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController(IPaymentService service) : ControllerBase
{

    [HttpGet("get-all-payment")]
    [PermissionAuthorize(Core.Permission.GetAllPaymentPermission)]
    public async Task<IActionResult> GetAll()
    {
        var result = await service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("get-by-id-payment/{id:int}")]
    [PermissionAuthorize(Core.Permission.GetByIdPaymentPermission)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpPost("create-payment")]
    [PermissionAuthorize(Core.Permission.CreatePaymentPermission)]
    public async Task<IActionResult> Create([FromBody] PaymentCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.CreateAsync(dto);
        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result);
        /*
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);*/
    }

    [HttpPut("update-payment-by-id/{id:int}")]
    [PermissionAuthorize(Core.Permission.UpdatePaymentPermission)]
    public async Task<IActionResult> Update(int id, [FromBody] PaymentUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.UpdateAsync(id, dto);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpDelete("delete-payment-by-id/{id:int}")]
    [PermissionAuthorize(Core.Permission.DeletePaymentPermission)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await service.DeleteAsync(id);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }
}
