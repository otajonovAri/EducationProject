using EducationApp.Application.DTOs.PaymentDto;
using EducationApp.Application.DTOs.StaffDto;
using EducationApp.Application.Responses;

namespace EducationApp.Application.Service.PaymentServices;

public interface IPaymentService
{
    Task<ApiResult<List<PaymentResponseDto>>> GetAllAsync();
    Task<ApiResult<PaymentResponseDto>> GetByIdAsync(int id);
    Task<ApiResult<object>> CreateAsync(PaymentCreateDto dto);
    Task<ApiResult<object>> UpdateAsync(int id, PaymentUpdateDto dto);
    Task<ApiResult<object>> DeleteAsync(int id);
}