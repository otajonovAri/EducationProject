using EducationApp.Application.DTOs.AttendanceDto;
using EducationApp.Application.DTOs.GroupDto;
using EducationApp.Application.Responses;

namespace EducationApp.Application.Service.AttendanceServices;

public interface IAttendanceService
{
    Task<ApiResult<List<AttendanceResponseDto>>> GetAllAsync();
    Task<ApiResult<AttendanceResponseDto>> GetByIdAsync(int id);
    Task<ApiResult<object>> CreateAsync(AttendanceCreateDto dto);
    Task<ApiResult<object>> UpdateAsync(int id, AttendanceUpdateDto dto);
    Task<ApiResult<object>> DeleteAsync(int id);
}