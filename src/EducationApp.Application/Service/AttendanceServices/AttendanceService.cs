using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApp.Application.DTOs.AttendanceDto;
using EducationApp.Application.Repositories.AttendanceRepository;
using EducationApp.Application.Responses;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Application.Service.AttendanceServices;

public class AttendanceService(IAttendanceRepository repo , IMapper mapper) : IAttendanceService
{
    public async Task<ApiResult<List<AttendanceResponseDto>>> GetAllAsync()
    {
        var attendances = await repo.GetAll()
            .ProjectTo<AttendanceResponseDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return new ApiResult<List<AttendanceResponseDto>>("Success", true, attendances);
    }

    public async Task<ApiResult<AttendanceResponseDto>> GetByIdAsync(int id)
    {
        var attendance = await repo.GetByIdAsync(id);

        if (attendance is null)
            return new ApiResult<AttendanceResponseDto>("Attendance Not Found", false, null!);

        return new ApiResult<AttendanceResponseDto>("Success", true, mapper.Map<AttendanceResponseDto>(attendance));
    }

    public async Task<ApiResult<object>> CreateAsync(AttendanceCreateDto dto)
    {
        var entity = mapper.Map<Core.Entities.Attendance>(dto);
        await repo.AddAsync(entity);
        await repo.SaveChangesAsync();
        return new ApiResult<object>("Attendance Created", true, $"{entity.Id}");
    }

    public async Task<ApiResult<object>> UpdateAsync(int id, AttendanceUpdateDto dto)
    {
        var existing = await repo.GetByIdAsync(id);

        if(existing is null)
            return new ApiResult<object>("Attendance Not Found", false, null!);

        mapper.Map(dto, existing);
        repo.Update(existing);
        await repo.SaveChangesAsync();

        return new ApiResult<object>("Attendance Updated Successfully", true, $"{existing.Id}");
    }

    public async Task<ApiResult<object>> DeleteAsync(int id)
    {
        var existing = await repo.GetByIdAsync(id);

        if(existing is null)
            return new ApiResult<object>("Attendance Not Found", false, null!);

        repo.Delete(existing);
        await repo.SaveChangesAsync();

        return new ApiResult<object>("Attendance Deleted Successfully", true, $"{existing.Id}");
    }
}