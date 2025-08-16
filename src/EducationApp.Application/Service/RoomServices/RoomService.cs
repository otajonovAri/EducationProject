using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApp.Application.DTOs.Room;
using EducationApp.Application.Repositories.RoomRepository;
using EducationApp.Application.Responses;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Application.Service.RoomServices;

public class RoomService(IRoomRepository repo , IMapper mapper) : IRoomService
{
    public async Task<ApiResult<List<RoomResponseDto>>> GetAllAsync()
    {
        var rooms = await repo.GetAll()
            .ProjectTo<RoomResponseDto>(mapper.ConfigurationProvider).ToListAsync();

        return new ApiResult<List<RoomResponseDto>>("Success", true, rooms);
    }

    public async Task<ApiResult<RoomResponseDto>> GetByIdAsync(int id)
    {
        var room = await repo.GetByIdAsync(id);
        if (room is null)
            return new ApiResult<RoomResponseDto>("Room not Found", false, null!);

        return new ApiResult<RoomResponseDto>("Success", true, mapper.Map<RoomResponseDto>(room));
    }

    public async Task<ApiResult<object>> CreateAsync(RoomCreateDto dto)
    {
        var entity = mapper.Map<Core.Entities.Room>(dto);

        await repo.AddAsync(entity);
        await repo.SaveChangesAsync();

        return new ApiResult<object>("Room created successfully", true, $"{entity.Id}");
    }

    public async Task<ApiResult<object>> UpdateAsync(int id, RoomUpdateDto dto)
    {
        var existing = await repo.GetByIdAsync(id);

        if (existing is null)
            return new ApiResult<object>("Success", false, null!);

        mapper.Map(dto, existing);
        repo.Update(existing);
        await repo.SaveChangesAsync();

        return new ApiResult<object>("Successfully updated room", true, $"{existing.Id}");
    }

    public async Task<ApiResult<object>> DeleteAsync(int id)
    {
        var existing = await repo.GetByIdAsync(id);
        
        if (existing is null)
            return new ApiResult<object>("Room Not Found", false, null!);

        repo.Delete(existing);
        await repo.SaveChangesAsync();

        return new ApiResult<object>("Room deleted successfully", true, null!);
    }
}