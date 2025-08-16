using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApp.Application.DTOs.SubjectDto;
using EducationApp.Application.Repositories.SubjectRepository;
using EducationApp.Application.Responses;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Application.Service.SubjectServices;

public class SubjectService(ISubjectRepository repo , IMapper mapper) : ISubjectService
{
    public async Task<ApiResult<List<SubjectResponseDto>>> GetAllAsync()
    {
        var subjects = await repo.GetAll()
            .ProjectTo<SubjectResponseDto>(mapper.ConfigurationProvider).ToListAsync();

        return new ApiResult<List<SubjectResponseDto>>("Success", true, subjects);
    }

    public async Task<ApiResult<SubjectResponseDto>> GetByIdAsync(int id)
    {
        var subject = await repo.GetByIdAsync(id);

        if (subject is null)
            return new ApiResult<SubjectResponseDto>("Subject Not Found", false, null!);

        return new ApiResult<SubjectResponseDto>("Success", true, mapper.Map<SubjectResponseDto>(subject));
    }

    public async Task<ApiResult<object>> CreateAsync(CreateSubjectDto dto)
    {
        var entity = mapper.Map<Core.Entities.Subject>(dto);
        
        await repo.AddAsync(entity);
        await repo.SaveChangesAsync();

        return new ApiResult<object>("Subject created successfully", true, $"{entity.Id}");
    }

    public async Task<ApiResult<object>> UpdateAsync(int id, SubjectUpdateDto dto)
    {
        var existing = await repo.GetByIdAsync(id);
        
        if (existing is null)
            return new ApiResult<object>("Subject Not Found", false, null!);
        
        mapper.Map(dto, existing);
        repo.Update(existing);
        await repo.SaveChangesAsync();

        return new ApiResult<object>("Successfully updated subject", true, $"{existing.Id}");
    }

    public async Task<ApiResult<object>> DeleteAsync(int id)
    {
        var existing = await repo.GetByIdAsync(id);
        
        if (existing is null)
            return new ApiResult<object>("Subject Not Found", false, null!);
        
        repo.Delete(existing);
        await repo.SaveChangesAsync();
        return new ApiResult<object>("Subject deleted successfully", true, null!);
    }
}