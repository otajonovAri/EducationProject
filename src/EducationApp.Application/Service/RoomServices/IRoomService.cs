using EducationApp.Application.DTOs.PaymentDto;
using EducationApp.Application.DTOs.Room;
using EducationApp.Application.Responses;

namespace EducationApp.Application.Service.RoomServices;

public interface IRoomService
{
    Task<ApiResult<List<RoomResponseDto>>> GetAllAsync();
    Task<ApiResult<RoomResponseDto>> GetByIdAsync(int id);
    Task<ApiResult<object>> CreateAsync(RoomCreateDto dto);
    Task<ApiResult<object>> UpdateAsync(int id, RoomUpdateDto dto);
    Task<ApiResult<object>> DeleteAsync(int id);
}