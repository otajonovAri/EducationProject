using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApp.Application.DTOs.GroupSubjectDto;
using EducationApp.Application.Repositories.GroupRepository;
using EducationApp.Application.Repositories.GroupSubjectRepository;
using EducationApp.Application.Responses;
using EducationApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Application.Service.GroupSubjectServices;

public class GroupSubjectService(IGroupSubjectRepository repo , IMapper mapper) : IGroupSubjectService
{
    public async Task<ApiResult<List<GroupSubjectResponseDto>>> GetAllAsync()
    {
        var groupSubjects = await repo.GetAll()
            .ProjectTo<GroupSubjectResponseDto>(mapper.ConfigurationProvider).ToListAsync();
        return new ApiResult<List<GroupSubjectResponseDto>>("Success", true, groupSubjects);
    }

    public async Task<ApiResult<GroupSubjectResponseDto>> GetByIdAsync(int id)
    {
        var groupSubject = await repo.GetByIdAsync(id);
        
        if (groupSubject is null)
            return new ApiResult<GroupSubjectResponseDto>("Group Subject not Found", false, null!);
        
        var dto = mapper.Map<GroupSubjectResponseDto>(groupSubject);
        return new ApiResult<GroupSubjectResponseDto>("Success", true, dto);
    }

    public async Task<ApiResult<object>> CreateAsync(GroupSubjectCreateDto dto)
    {
        var entity = mapper.Map<GroupSubject>(dto);
        
        await repo.AddAsync(entity);
        await repo.SaveChangesAsync();
        
        return new ApiResult<object>("Group Subject Created", true, $"{entity.Id}");
    }

    public async Task<ApiResult<object>> UpdateAsync(int id, GroupSubjectUpdateDto dto)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing is null)
            return new ApiResult<object>("Group Subject not Found", false, null!);
        mapper.Map(dto, existing);
        repo.Update(existing);
        await repo.SaveChangesAsync();
        return new ApiResult<object>("Group Subject Updated Successfully", true, $"{existing.Id}");
    }

    public async Task<ApiResult<object>> DeleteAsync(int id)
    {
        var existing = await repo.GetByIdAsync(id);
        
        if (existing is null)
            return new ApiResult<object>("Group Subject not Found", false, null!);
        
        repo.Delete(existing);
        await repo.SaveChangesAsync();
        
        return new ApiResult<object>("Group Subject Deleted Successfully", true, null!);
    }
}