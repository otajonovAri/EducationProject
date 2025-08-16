using EducationApp.Application.DTOs.StaffDto;
using EducationApp.Application.Responses;

namespace EducationApp.Application.Service.StaffServices;

public interface IStaffService
{
    Task<ApiResult<List<StaffResponseDto>>> GetAllAsync();
    Task<int> GetByConditionStaffCount();
    Task<ApiResult<List<StaffGetAllResponseDto>>> GetByConditionAllActiveStaff();
    Task<ApiResult<List<StaffGetAllResponseDto>>> GetByConditionAllTeachers();
    Task<ApiResult<List<StaffGetAllResponseDto>>> GetByConditionAllOtherStaff();
    Task<ApiResult<StaffResponseDto>> GetByIdAsync(int id);
    Task<ApiResult<object>> CreateAsync(StaffCreateDto dto);
    Task<ApiResult<object>> UpdateAsync(int id, StaffUpdateDto dto);
    Task<ApiResult<object>> DeleteAsync(int id);


}