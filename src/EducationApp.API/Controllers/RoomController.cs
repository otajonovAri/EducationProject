using EducationApp.Application.Auth;
using EducationApp.Application.DTOs.Room;
using EducationApp.Application.Service.RoomServices;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomController(IRoomService service) : ControllerBase
{
    [HttpGet("get-all-room")]
    [PermissionAuthorize(Core.Permission.GetAllRoomPermission)]
    public async Task<IActionResult> GetAllAsync() 
        => Ok(await service.GetAllAsync());

    [HttpGet("get-by-id-room/{id:int}")]
    [PermissionAuthorize(Core.Permission.GetByIdRoomPermission)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await service.GetByIdAsync(id);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpPost("create-room")]
    [PermissionAuthorize(Core.Permission.CreateRoomPermission)]
    public async Task<IActionResult> CreateAsync([FromBody] RoomCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.CreateAsync(dto);
        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result);
        //return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Data }, result);
    }

    [HttpPut("update-room-by-id/{id:int}")]
    [PermissionAuthorize(Core.Permission.UpdateRoomPermission)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] RoomUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.UpdateAsync(id, dto);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpDelete("delete-room-by-id/{id:int}")]
    [PermissionAuthorize(Core.Permission.DeleteRoomPermission)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await service.DeleteAsync(id);
        if (!result.Success)
            return NotFound(result.Message);
        return Ok(result);
    }
}
