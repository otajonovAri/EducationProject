using EducationApp.Application.DTOs.StudentDto;
using EducationApp.Application.DTOs.UserDto;
using EducationApp.Application.Responses;

namespace EducationApp.Application.Service.StudentServices;

public interface IStudentService
{
    Task<ApiResult<List<StudentResponseDto>>> GetAllAsync();
    Task<ApiResult<StudentResponseDto>> GetByIdAsync(int id);
    Task<int> GetByConditionCountActiveStudents();
    Task<int> GetByConditionCountUnpaidStudents();
    Task<int> GetByConditionPaidStudentsOnThisMonth();
    Task<int> GetByConditionCountDroppedOutStudentsActiveGroup();
    Task<ApiResult<bool>> GetByConditionReturnLastPayment(int studentId);
    Task<ApiResult<List<StudentGetAllResponseDto>>> GetByConditionAllActiveStudents();
    Task<ApiResult<List<StudentGetAllResponseDto>>> GetByConditionAllInActiveStudents();
    Task<ApiResult<object>> CreateAsync(StudentCreateDto dto);
    Task<ApiResult<object>> UpdateAsync(int id, StudentUpdateDto dto);
    Task<ApiResult<object>> DeleteAsync(int id);
}