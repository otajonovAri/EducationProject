using EducationApp.Application.DTOs.UserDto;
using EducationApp.Application.Responses;

namespace EducationApp.Application.Service.UserServices;

public interface IUserService
{
    Task<ApiResult<List<UserResponseDto>>> GetAllAsync();
    Task<ApiResult<UserResponseDto>> GetByIdAsync(int id);
    Task<ApiResult<string>> VerifyOtpAsync(OtpVerificationModel model);
    Task<ApiResult<object>> CreateAsync(UserCreateDto dto);
    Task<ApiResult<object>> UpdateAsync(int id, UserUpdateDto dto);
    Task<ApiResult<object>> DeleteAsync(int id);
}