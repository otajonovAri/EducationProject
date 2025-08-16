using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApp.Application.DTOs.PaymentDocumentDto;
using EducationApp.Application.Repositories.PaymentDocumentRepository;
using EducationApp.Application.Responses;
using EducationApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Application.Service.PaymentDocumentServices;

public class PaymentDocumentService(IPaymentDocumentRepository repo , IMapper mapper) 
    : IPaymentDocumentService
{
    public async Task<ApiResult<List<PaymentDocumentResponseDto>>> GetAllAsync()
    {
        var documents = await repo.GetAll()
            .ProjectTo<PaymentDocumentResponseDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return new ApiResult<List<PaymentDocumentResponseDto>>("Payment documents retrieved successfully", true, documents);
    }

    public async Task<ApiResult<PaymentDocumentResponseDto>> GetByIdAsync(int id)
    {
        var paymentDocs = await repo.GetByIdAsync(id);

        if(paymentDocs is null) 
            return new ApiResult<PaymentDocumentResponseDto>("Payment Document Not Found", false, null!);

        return new ApiResult<PaymentDocumentResponseDto>("Success", true, mapper.Map<PaymentDocumentResponseDto>(paymentDocs));
    }

    public async Task<ApiResult<object>> CreateAsync(PaymentDocumentCreateDto dto)
    {
        var entity = mapper.Map<PaymentDocument>(dto);
        await repo.AddAsync(entity);
        await repo.SaveChangesAsync();
        return new ApiResult<object>("Payment document created successfully", true, $"{entity.Id}");
    }

    public async Task<ApiResult<object>> UpdateAsync(int id, PaymentDocumentUpdateDto dto)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing is null)
            return new ApiResult<object>("Payment Document Not Found", false, null!);
        mapper.Map(dto, existing);
        repo.Update(existing);
        await repo.SaveChangesAsync();
        return new ApiResult<object>("Payment document updated successfully", true, $"{existing.Id}");
    }

    public async Task<ApiResult<object>> DeleteAsync(int id)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing is null)
            return new ApiResult<object>("Payment Document Not Found", false, null!);
        
        repo.Delete(existing);
        await repo.SaveChangesAsync();
        return new ApiResult<object>("Payment document deleted successfully", true, $"{existing.Id}");
    }
}