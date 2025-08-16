using EducationApp.Application.DTOs.GroupDto;
using EducationApp.Application.DTOs.GroupSubjectDto;
using EducationApp.Application.Responses;

namespace EducationApp.Application.Service.GroupSubjectServices;

public interface IGroupSubjectService
{
    Task<ApiResult<List<GroupSubjectResponseDto>>> GetAllAsync();
    Task<ApiResult<GroupSubjectResponseDto>> GetByIdAsync(int id);
    Task<ApiResult<object>> CreateAsync(GroupSubjectCreateDto dto);
    Task<ApiResult<object>> UpdateAsync(int id, GroupSubjectUpdateDto dto);
    Task<ApiResult<object>> DeleteAsync(int id);
}