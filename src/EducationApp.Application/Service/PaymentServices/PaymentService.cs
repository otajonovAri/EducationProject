using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApp.Application.DTOs.PaymentDto;
using EducationApp.Application.Repositories.PaymentRepository;
using EducationApp.Application.Responses;
using EducationApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Application.Service.PaymentServices;

public class PaymentService(IPaymentRepository repo , IMapper mapper) : IPaymentService
{
    public async Task<ApiResult<List<PaymentResponseDto>>> GetAllAsync()
    {
        var payments = await repo.GetAll()
            .ProjectTo<PaymentResponseDto>(mapper.ConfigurationProvider)
            .ToListAsync();
       
        return new ApiResult<List<PaymentResponseDto>>("Payments retrieved successfully", true, payments);
    }

    public async Task<ApiResult<PaymentResponseDto>> GetByIdAsync(int id)
    {   
        var payment = await repo.GetByIdAsync(id);
        if (payment is null)
            return new ApiResult<PaymentResponseDto>("Payment Not Found", false, null!);

        return new ApiResult<PaymentResponseDto>("Success", true, mapper.Map<PaymentResponseDto>(payment));
    }

    public async Task<ApiResult<object>> CreateAsync(PaymentCreateDto dto)
    {
        var entity = mapper.Map<Payment>(dto);
        await repo.AddAsync(entity);
        await repo.SaveChangesAsync();
        return new ApiResult<object>("Payment created successfully", true, $"{entity.Id}");
    }

    public async Task<ApiResult<object>> UpdateAsync(int id, PaymentUpdateDto dto)
    {
        var existing = await repo.GetByIdAsync(id);
        
        if (existing is null)
            return new ApiResult<object>("Payment Not Found", false, null!);
        
        mapper.Map(dto, existing);
        repo.Update(existing);
        await repo.SaveChangesAsync();

        return new ApiResult<object>("Payment updated successfully", true, $"{existing.Id}");
    }

    public async Task<ApiResult<object>> DeleteAsync(int id)
    {
        var existing = await repo.GetByIdAsync(id);
        
        if (existing is null)
            return new ApiResult<object>("Payment Not Found", false, null!);
        
        repo.Delete(existing);
        await repo.SaveChangesAsync();
        return new ApiResult<object>("Payment deleted successfully", true, $"{existing.Id}");
    }
}