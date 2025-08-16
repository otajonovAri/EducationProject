using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApp.Application.DTOs.StaffSubjectDto;
using EducationApp.Application.Repositories.StaffSubjectRepository;
using EducationApp.Application.Responses;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Application.Service.StaffSubjectServices;

public class StaffSubjectService(IStaffSubjectRepository repo , IMapper mapper) : IStaffSubjectService
{
    public async Task<ApiResult<List<StaffSubjectResponseDto>>> GetAllAsync()
    {
        var staffSubjects = await repo.GetAll()
            .ProjectTo<StaffSubjectResponseDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return new ApiResult<List<StaffSubjectResponseDto>>("Success", true, staffSubjects);
    }

    public async Task<ApiResult<StaffSubjectResponseDto>> GetByIdAsync(int id)
    {
        var staffSubject = await repo.GetByIdAsync(id);
        
        if (staffSubject is null)
            return new ApiResult<StaffSubjectResponseDto>("Staff Subject not Found", false, null!);

        return new ApiResult<StaffSubjectResponseDto>("Success", true, mapper.Map<StaffSubjectResponseDto>(staffSubject));
    }

    public async Task<ApiResult<object>> CreateAsync(StaffSubjectCreateDto dto)
    {
        var entity = mapper.Map<Core.Entities.StaffSubject>(dto);
        await repo.AddAsync(entity);
        await repo.SaveChangesAsync();
        return new ApiResult<object>("Staff Subject Created", true, $"{entity.Id}");
    }

    public async Task<ApiResult<object>> UpdateAsync(int id, StaffSubjectUpdateDto dto)
    {
        var existing = await repo.GetByIdAsync(id);
        
        if (existing is null)
            return new ApiResult<object>("Staff Subject not Found", false, null!);

        mapper.Map(dto, existing);
        repo.Update(existing);
        await repo.SaveChangesAsync();
        
        return new ApiResult<object>("Staff Subject Updated Successfully", true, $"{existing.Id}");
    }

    public async Task<ApiResult<object>> DeleteAsync(int id)
    {
        var existing = await repo.GetByIdAsync(id);
        
        if (existing is null)
            return new ApiResult<object>("Staff Subject not Found", false, null!);

        repo.Delete(existing);
        await repo.SaveChangesAsync();
        
        return new ApiResult<object>("Staff Subject Deleted Successfully", true, $"{existing.Id}");
    }
}