using EducationApp.Application.DTOs.StaffDto;
using EducationApp.Application.DTOs.StaffSubjectDto;
using EducationApp.Application.Responses;

namespace EducationApp.Application.Service.StaffSubjectServices;

public interface IStaffSubjectService
{
    Task<ApiResult<List<StaffSubjectResponseDto>>> GetAllAsync();
    Task<ApiResult<StaffSubjectResponseDto>> GetByIdAsync(int id);
    Task<ApiResult<object>> CreateAsync(StaffSubjectCreateDto dto);
    Task<ApiResult<object>> UpdateAsync(int id, StaffSubjectUpdateDto dto);
    Task<ApiResult<object>> DeleteAsync(int id);
}