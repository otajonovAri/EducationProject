using EducationApp.Application.DTOs.PaymentDocumentDto;
using EducationApp.Application.DTOs.PaymentDto;
using EducationApp.Application.Responses;

namespace EducationApp.Application.Service.PaymentDocumentServices;

public interface IPaymentDocumentService
{
    Task<ApiResult<List<PaymentDocumentResponseDto>>> GetAllAsync();
    Task<ApiResult<PaymentDocumentResponseDto>> GetByIdAsync(int id);
    Task<ApiResult<object>> CreateAsync(PaymentDocumentCreateDto dto);
    Task<ApiResult<object>> UpdateAsync(int id, PaymentDocumentUpdateDto dto);
    Task<ApiResult<object>> DeleteAsync(int id);
}