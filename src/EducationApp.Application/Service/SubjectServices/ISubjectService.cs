using EducationApp.Application.DTOs.SubjectDto;
using EducationApp.Application.Responses;

namespace EducationApp.Application.Service.SubjectServices;

public interface ISubjectService
{
    Task<ApiResult<List<SubjectResponseDto>>> GetAllAsync();
    Task<ApiResult<SubjectResponseDto>> GetByIdAsync(int id);
    Task<ApiResult<object>> CreateAsync(CreateSubjectDto dto);
    Task<ApiResult<object>> UpdateAsync(int id,  SubjectUpdateDto dto);
    Task<ApiResult<object>> DeleteAsync(int id);
}